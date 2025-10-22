var facilityAccessReportModule = (function () {
    var privateSettings;
    return {
        settings: {
            accessTable: '',
            accessStorage: '',
            dataTableOptions: {
                tableSelector: '#access-search-results',
                URL: '/facilityaccessreport',
                scrollToContainerSelector: '#accessResultsGrid',
                initialOrder: [[0, 'desc']],
                searchFilterFormSelector: 'form[name="access-search-filters"]',
                columnDefs: [
                    {
                        "className": "text-center",
                        "targets": "_all"
                    }
                ],
                columns: [
                    {
                        data: 'EntryDateTime', type: 'date', className: 'text-center', autoWidth: true,
                        render: function (data) {
                            return facilityAccessReportModule.formatDate(data, true);
                        }
                    },
                    {
                        data: 'CategoryName', autoWidth: true
                    },                    
                    {
                        data: 'EmployeeFullName', autoWidth: true,
                        render: function (nTd, sData, oData, iRow, iCol) {
                            var viewImage = '<button type="button" class="btn btn-primary w-100" data-bs-toggle="modal" data-bs-target="#employeeModal" data-bs-name="' +
                                oData.EmployeeFullName + '" data-bs-email="' + oData.Employee.WorkEmail
                                + '" data-bs-title="' + oData.Employee.WorkTitle
                                + '" data-bs-startdate="' + oData.Employee.StartDate
                                + '" data-bs-birthdate="' + oData.Employee.BirthDate
                                + '" data-bs-keycard="' + oData.Employee.KeyCardId
                                + '" data-bs-picture="data:image/jpg;base64,' + oData.Employee.ProfilePicture.ImageData + '.' + oData.Employee.ProfilePicture.ImageExtension
                                + '" data-bs-status="' + oData.Employee.ActiveStatus + '">' + oData.EmployeeFullName + '</button > ';
                            return viewImage;
                        }
                    },
                    {
                        data: 'SecurityImage', autoWidth: true, orderable: false,
                        render: function (nTd, sData, oData, iRow, iCol) {
                            var viewImage = '<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#imageModal" data-bs-baseimage="data:image/jpg;base64,' +
                                oData.SecurityImage.ImageData + '.' + oData.SecurityImage.ImageExtension + '" data-bs-employee="' + oData.EmployeeFullName
                                + '" data-bs-timestamp="' + oData.EntryDateTime
                                + '" data-bs-entrytype="' + oData.CategoryName + '">View Security Image</button > ';
                            return viewImage;
                        }
                    },
                    //{ data: 'KeyCardId', autoWidth: true },
                    { data: 'ScannerId', autoWidth: true }
                ]
            },            
        },
        init: function () {
            privateSettings = this.settings;
            this.setupPage();

            //Event Handlers            
            $(document).on('click', '[data-action="search"]', this.drawTable);
            $(document).on('click', 'button[type="reset"]', this.resetFilters);
        },
        formatDate: function (data, includeTime = true) {
            if (includeTime) {
                return moment(data).utc().format('lll');
            } else {
                return moment(data).utc().format('ll');
            }
        },
        imageModalEventSetup: function () {
            const imageModal = document.getElementById('imageModal')
            if (imageModal) {
                imageModal.addEventListener('show.bs.modal', event => {  
                    const button = event.relatedTarget
                    const employeeName = button.getAttribute('data-bs-employee');
                    const entryType = button.getAttribute('data-bs-entrytype');
                    const timeStamp = button.getAttribute('data-bs-timestamp');
                    const imageSrc = button.getAttribute('data-bs-baseimage')

                    const modalHeaderEmployee = imageModal.querySelector('#imageModalLabel');
                    modalHeaderEmployee.innerHTML = employeeName;

                    const modalEntryType = imageModal.querySelector('#entryType');
                    modalEntryType.innerHTML = entryType;

                    const modalTimestamp = imageModal.querySelector('#timeStamp');
                    modalTimestamp.innerHTML = facilityAccessReportModule.formatDate(timeStamp);

                    const modalBodyInput = imageModal.querySelector('.modal-body #baseImage')                    
                    modalBodyInput.setAttribute('src', imageSrc);
                })
            }
        },
        employeeModalEventSetup: function () {
            const imageModal = document.getElementById('employeeModal')
            if (imageModal) {
                imageModal.addEventListener('show.bs.modal', event => {
                    const button = event.relatedTarget
                    const employeeName = button.getAttribute('data-bs-name');
                    const email = button.getAttribute('data-bs-email');
                    const title = button.getAttribute('data-bs-title');
                    const startDate = button.getAttribute('data-bs-startdate')
                    const birthDate = button.getAttribute('data-bs-birthdate')
                    const keyCard = button.getAttribute('data-bs-keycard')
                    const picture = button.getAttribute('data-bs-picture')
                    const status = button.getAttribute('data-bs-status')

                    const modalHeaderEmployee = imageModal.querySelector('#employeeModalLabel');
                    modalHeaderEmployee.innerHTML = employeeName;

                    const modalEmail = imageModal.querySelector('.modal-body #emailLabel');
                    modalEmail.value = email;

                    const modalTitle = imageModal.querySelector('.modal-body #titleLabel');
                    modalTitle.value = title;

                    const modalKeyCard = imageModal.querySelector('.modal-body #keyCardLabel');
                    modalKeyCard.value = keyCard;

                    const modalBirthDate = imageModal.querySelector('.modal-body #birthDateLabel');
                    modalBirthDate.value = facilityAccessReportModule.formatDate(birthDate, false);

                    const modalStatus = imageModal.querySelector('.modal-body #activeLabel');
                    modalStatus.value = status.toUpperCase();

                    const modalStartDate = imageModal.querySelector('.modal-body #workStartDateLabel');
                    modalStartDate.value = facilityAccessReportModule.formatDate(startDate, false);

                    const modalBodyInput = imageModal.querySelector('.modal-body #profilePicture')
                    modalBodyInput.setAttribute('src', picture);
                })
            }
        },
        setupPage: function () {            
            privateSettings.accessTable = $().datatablesModule(privateSettings.dataTableOptions, function () {});
            facilityAccessReportModule.imageModalEventSetup();
            facilityAccessReportModule.employeeModalEventSetup();            
            facilityAccessReportModule.setupDataPickers();
        },
        setupDataPickers: function () {            
            flatpickr("#EntryDateTimeBegin", {
                enableTime: true,
                dateFormat: "m-d-Y H:i",
            });
            flatpickr("#EntryDateTimeEnd", {
                enableTime: true,
                dateFormat: "m-d-Y H:i",
            });
            $('#EntryDateTimeBeginClear').on('click', function () { $('#EntryDateTimeBegin').val(''); });
            $('#EntryDateTimeEndClear').on('click', function () { $('#EntryDateTimeEnd').val(''); });
        },
        resetFilters: function (el) {
            el.preventDefault();
            $(privateSettings.dataTableOptions.searchFilterFormSelector)[0].reset();
            facilityAccessReportModule.drawTable(el);
            return false;
        },
        drawTable: function (el) {
            el.preventDefault();
            privateSettings.accessTable.DataTableRedraw();
            return false;
        },
    };
})(window);


$(function () {
    facilityAccessReportModule.init();
});
