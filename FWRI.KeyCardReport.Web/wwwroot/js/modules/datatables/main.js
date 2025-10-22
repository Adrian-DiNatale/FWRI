$.fn.datatablesModule = function (options, ajaxCallback) {
    var root = this;
    var privateSettings = {
        tableinstance: '',
        tableSelector: '',
        initialOrder: '',
        URL: '',
        searchFilterFormSelector: '',
        columns: '',
        columnsDefs: '',
        scrollToContainerSelector: '',
        pageSize: 50,
        processingText: 'processing',
        footerCallback: '',
        dom: '<lip<"data-loader-wrapper"rt>p>'
    };

    this.Construct = function (options, callback) {
        $.extend(privateSettings, options);
        privateSettings.pageSize = parseInt($(privateSettings.searchFilterFormSelector).find('#Length').val());
        DataTable.defaults.column.orderSequence = ['asc', 'desc'];        
        $.fn.dataTable.ext.errMode = 'none';
        root.DataTableInit(callback);       
    };

    this.DataTableInit = function (ajaxCallback) {
        if ($(privateSettings.tableSelector).length !== 0) {
            var sortColumn = $(privateSettings.tableSelector + ' thead th').closest('th[data-sort=' + $(privateSettings.searchFilterFormSelector).find('#SortColumn').val() + ']').index();
            var sortDir = $(privateSettings.searchFilterFormSelector).find('#SortColumnDir').val();

            if (sortColumn === -1) {
                sortColumn = 0;
            }

            if (sortDir === undefined) {
                sortDir = 'asc';
            }

            privateSettings.tableinstance = $(privateSettings.tableSelector).DataTable({
                dom: privateSettings.dom,
                fixedHeader: {
                    header: true,
                    headerOffset: $('#header').height()
                },
                paging: true,
                pageLength: parseInt(privateSettings.pageSize),
                lengthChange: false,
                responsive: true,
                colReorder: false,
                autoWidth: true,
                language: {
                    processing: '<span id="page-load-spinner" class="spinner"></span>'
                },
                processing: true,
                serverSide: true,
                displayStart: parseInt($('#Start').val()),
                order: [[sortColumn, sortDir]],
                orderSequence: ['asc', 'desc'],
                footerCallback: privateSettings.footerCallback,
                deferRender: false,
                stateSave: false,
                ajax: {
                    timeout: 120000,
                    url: privateSettings.URL,
                    type: 'GET',
                    datatype: 'JSON',
                    data:
                        function (d) {
                            var ordering = d.columns[0].data;
                            var direction = 'desc';
                            if (d.order.length !== 0) {
                                ordering = d.columns[d.order[0].column].data;
                                direction = d.order[0].dir;
                            }
                            $(privateSettings.searchFilterFormSelector).find('#Start').val(parseInt(d['start']));
                            $(privateSettings.searchFilterFormSelector).find('#Length').val(parseInt(d['length']));
                            $(privateSettings.searchFilterFormSelector).find('#SortColumn').val(ordering);
                            $(privateSettings.searchFilterFormSelector).find('#SortColumnDir').val(direction);
                            let frm_data = $(privateSettings.searchFilterFormSelector).serializeArray();
                            return frm_data;
                        }
                },
                columns: privateSettings.columns,
                columnDefs: privateSettings.columnDefs
            }).on('xhr.dt', function (e, settings, json, xhr) {
                if (ajaxCallback !== undefined) {
                    ajaxCallback(json);
                }
            })
            .on('error.dt', function (e, settings, techNote, message) {
                console.log('An error has been reported by DataTables: ', message);
            });           
        };
    };


    this.DataTableRedraw = function () {
        privateSettings.tableinstance.draw();
    };

    this.Construct(options, ajaxCallback);

    return root;
};
