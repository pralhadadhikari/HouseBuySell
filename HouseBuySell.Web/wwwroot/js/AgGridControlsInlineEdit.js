/*
Function parameters:
1. GridID
2. ModelData
3. EditLink
4. ExcludeColumns : List of column names separated by a comma 'Createddate, ModifiedDate'
5.

*/



var CreateAgInlineGrid = function (options) {
    var config = {
        GridID: '',
        ModelData: '',
        EditLink: '',
        ExcludeColumns: '',
        editType: '',
        editable: '',
        api: ''
    };

    $.extend(config, options);


    var $ajaxCall = function (data, url, type, success, error) {
        $.ajax({
            type: type,
            async: true,
            url: url,
            data: data,
            success: success,
            error: error
        });
    };

    var grid = function () {

        var columnHeader = Object.keys(config.ModelData[0]);
        var columnDefsVal = []
        var tempRowData = []
        var currentEditingRow = '';

        columnHeader.forEach(function (key, value) {

            if (config.ExcludeColumns.search(key) < 0) { //True => Column not found in ExcludeColumns

                if (key != 'Id') {
                    columnDefsVal[value] = { headerName: key, field: key, editable: config.editable };
                } else {
                    //Add new column for adding Edit actions on ID column
                    columnDefsVal[value] = {
                        headerName: 'Actions',
                        field: key,
                        cellRenderer: function (params) {
                            return config.editLink.replace('0', params.value);
                        }
                    };
                }
            }
        });


        // let the grid know which columns and what data to use
        var gridOptions = {
            columnDefs: columnDefsVal,
            rowData: config.ModelData,
            enableSorting: true,
            enableFilter: true,
            pagination: true,
            paginationAutoPageSize: false,
            paginationPageSize: 3,
            /* Group columns */
            groupHeaderHeight: 55,

            /* Label columns */
            headerHeight: 50,

            /* Floating filter */
            floatingFiltersHeight: 50,

            /* Pivoting, requires turning on pivot mode. Label columns */
            pivotGroupHeaderHeight: 50,

            /* Pivoting, requires turning on pivot mode. Group columns */
            pivotGroupHeaderHeight: 100,
            rowHeight: 42,
            editType: config.editType //cofig.editType
            
        };

        if (config.editType == 'cell') {
            gridOptions.onCellEditingStarted = function (event) {},
            gridOptions.onCellValueChanged = function (event) {
                var data = event.data;
                $ajaxCall(data, config.api, 'post', successCB, errorCB);
            }
        }

        if (config.editType == 'fullRow') {
            gridOptions.onRowEditingStarted = function (event) { },
            gridOptions.onRowValueChanged = function (event) {
                var data = event.data;
                $ajaxCall(data, config.api, 'post', successCB, errorCB);
            }
        }

        var successCB = function (data) {
            console.log(data)
        }
        
        var errorCB = function (data) {
            console.log('error')
            //location.reload();

            // don't persist the new changes if error occurs
            // find a way to 
        }

       
        //var setDataOnRow = function () {
        //    var cellDefs = gridOptions.api.getEditingCells();
        //    cellDefs.forEach(function (cellDef) {
        //        console.log(cellDef.rowIndex);
        //        currentEditingRow = cellDef.rowIndex;
        //    });
        //}

        var createGrid = function () {

            // lookup the container we want the Grid to use
            var eGridDiv = document.querySelector('#' + config.GridID);

            // create the grid passing in the div to use together with the columns & data we want to use
            new agGrid.Grid(eGridDiv, gridOptions);
        }
        

        var onBtStopEditing = function () {
            gridOptions.api.stopEditing();
        }

        var onBtStartEditing = function () {
            gridOptions.api.setFocusedCell(1);
        }


        return {
            onBtStopEditing: onBtStopEditing,
            onBtStartEditing: onBtStartEditing,
            createGrid: createGrid
        }
        
    }();

    var globalEvents = function () {

        grid.createGrid();

        $(document).off('click', '.startEdit').on('click', '.startEdit', function () {
            grid.onBtStartEditing();
        });

        $(document).off('click', '.stopEdit').on('click', '.stopEdit', function () {
            grid.onBtStopEditing();
        });
    }();
};




        