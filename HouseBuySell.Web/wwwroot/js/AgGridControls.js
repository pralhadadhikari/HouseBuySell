/*
Function parameters:
1. GridID
2. ModelData
3. EditLink
4. ExcludeColumns : List of column names separated by a comma 'Createddate, ModifiedDate'
5.

*/
function CreateAgGrid(GridID, ModelData, EditLink, ExcludeColumns = '') { 	
	var columnHeader = Object.keys(ModelData[0]);
	var columnDefsVal = []

	columnHeader.forEach(function (key, value) {                    		

		if (ExcludeColumns.search(key) < 0) { //True => Column not found in ExcludeColumns

			if (key != 'Id') {
				columnDefsVal[value] = { headerName: key, field: key };
			} else {
				//Add new column for adding Edit actions on ID column
				columnDefsVal[value] = {
					headerName: 'Actions',
					field: key,
					cellRenderer: function (params) {
						return editLink.replace('0', params.value);
					}

				};
			}

		}

	});

	// let the grid know which columns and what data to use
	var gridOptions = {
		columnDefs: columnDefsVal,
		rowData: ModelData,
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
		rowHeight: 42
	};

	// lookup the container we want the Grid to use
	var eGridDiv = document.querySelector('#' + GridID);

	// create the grid passing in the div to use together with the columns & data we want to use
	new agGrid.Grid(eGridDiv, gridOptions);

	}

        