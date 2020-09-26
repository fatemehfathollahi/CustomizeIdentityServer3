define(['ko', 'jquery', 'jqueryDataTables', 'dataTablesBootstrap', 'styler'], function (ko, $, jqueryDataTables, dataTablesBootstrap, styler) {
    //این سی اس اس اگر اضافه شود جدول را بهم میریزد
    //styler.load('/Content/jquery.dataTables.css');

    ko.bindingHandlers.dataTable = {
        init: function (element, valueAccessor, allBinding, viewModel, bindingContext) {
            if ($.data(element, isInitialisedKey) === true) return;

            var binding = ko.utils.unwrapObservable(valueAccessor());
            var isInitialisedKey = "ko.bindingHandlers.dataTable.isInitialised";
            var options = {};

            // ** Initialise the DataTables options object with the data-bind settings **
            // Clone the options object found in the data bindings.
            // This object will form the base for the DataTable initialisation object.
            //agar options ra defualt bedahad inja vared mikonim
            if (binding.options) options = $.extend(options, binding.options);

            if (!binding.gridId) {
                throw ('Table Id not set!')
            }

            var tableId = binding.gridId;

            if (binding.setDefualtValue !== false) {
                options = {
                    //bJQueryUI: false,

                    //if this line uncomment, raise fnInit error
                    //sPaginationType: "bootstrap",
                    sDom: '<rt><"bottom"ip>',
                    aLengthMenu: [[25, 50, 100, -1], [25, 50, 100, 'همه']],
                    iDisplayLength: 10,
                    retrieve: true,
                    bProcessing: true,
                    bServerSide: true,
                    bDestroy: true,
                    bPaginate: true,
                    //sAjaxSource: mainUrl,
                    bAutoWidth: false,
                    oLanguage: {
                        //"sUrl": "media/language/de_DE.txt",
                    	"sProcessing": "Processing...",
                        "sLengthMenu": "Number Of Record _MENU_",
                        "sZeroRecords": "",
                        "sEmptyTable": "",
                        "sInfo": "Show _START_ To _END_ From _TOTAL_ Record",
                        "sInfoEmpty": "Empty",
                        "sInfoFiltered": "(Filter _MAX_ Record)",
                        "sInfoPostFix": "",
                        "sSearch": "Search:",
                        "sUrl": "",
                        "oPaginate": {
                            "sFirst": "First",
                            "sPrevious": "Previous",
                            "sNext": "Next",
                            "sLast": "Last"
                        }
                    },
                    fnServerParams: function (aoData) {
                        //var FilterData = new Object(binding.serverParamsArray);
                        //FilterData.CustomerName = $.trim($("#CustomerName").val());
                        //FilterData.BranchName = $.trim($("#BranchName").val());
                        //aoData.push({ name: "FilterData", value: JSON.stringify(FilterData) });

                        if (binding.serverParamsArray && binding.serverParamsArray.length) {
                            filterData = new Object();
                            ko.utils.arrayForEach(binding.serverParamsArray, function (col) {
                                //implement with jquery
                                //{ propertyName: 'CustomerName', idName: 'txtOriginalAmountFrom',type:'text'}
                                // if (col.type == 'text') {
                                //filterData[col.propertyName] = $.trim($('#' + col.idName).val());
                                //aoData.push({ name: col.propertyName, value: $('#' + col.idName).val() });
                                // }

                                //implement with ko
                                filterData[col.propertyName] = col.observableValue();
                                aoData.push({ name: col.propertyName, value: col.observableValue() });
                            });

                            aoData.push({ name: "FilterData", value: JSON.stringify(filterData) });
                        }

                        //if necessary enter this info for retreive and use in apiController Method
                        //aoData.push({ name: "UserName", value: $('#txtUserName').val() });
                        //aoData.push({ name: "FirstName", value: $('#txtFirstName').val() });
                        //aoData.push({ name: "LastName", value: $('#txtLastName').val() });
                        //aoData.push({ name: "MelliCode", value: $('#txtMelliCode').val() });
                    },
                    fnCreatedRow: function (nRow, aData, iDataIndex) {
                        //Add Id to row for fetching selected row id
                        RowIdName = 'ID';
                        if (binding.idColumnName) {
                            RowIdName = binding.idColumnName;
                        }
                        $(nRow).attr('RowID', aData[RowIdName]);
                    },
                    fnDrawCallback: function () {
                        //اگر روی یک ردیف از دیتا تیبل کلیک شود این رویداد رخ داده
                        //را از باقی ردیف ها حذف کرده و به ردیف جاری درج می کند selected کلاس
                        $('#' + tableId + ' tbody').on('click', 'tr', function () {
                            //$('#' + tableId + ' tbody tr').click(function () {
                            if (binding.multiselect !== true) {
                                $('#' + tableId + ' tbody tr').each(function () {
                                    $(this).removeClass("selected");
                                    $(this).find('input:checkbox').prop('checked', false);
                                });
                            }

                            //وضعیت چک باکس را می گیریم و آن را برعکس می کنیم
                            //وقتی وضعیت چند انتخابی است باید از این راه برویم چون وضعیت ردیف جاری را باید برعکس کنیم
                            var checkStatus = $(this).find('input:checkbox').prop('checked');
                            $(this).find('input:checkbox').prop('checked', !checkStatus);
                            $(this).toggleClass("selected");

                            //get first selected row  id
                            var row = $('#' + tableId + ' tr.selected');
                            if (!row.closest('tr').attr('RowID')) {
                                throw new Error('Current row is undefined');
                            }

                            binding.selectedRowId(row.closest('tr').attr('RowID'));
                            //get selected rows  collection
                            binding.selectedRowsCollection($('#' + tableId + ' tr.selected'));
                        });
                    },
                };
            };
            if (binding.sDom) {
                options.sDom = binding.sDom;
            }
            if (binding.pagination === false) {
                options.bPaginate = false;
            }
            if (binding.aLengthMenu) {
                options.aLengthMenu = binding.aLengthMenu;
            }

            if (binding.iDisplayLength) {
                options.iDisplayLength = binding.iDisplayLength;
            }

            //chon defualt true ast agar false shavad meghdardehi mishavad
            if (binding.retrieve === false) {
                options.retrieve = binding.retrieve;
            }
            if (binding.bProcessing === false) {
                options.bProcessing = binding.bProcessing;
            }
            if (binding.bServerSide === false) {
                options.bServerSide = binding.bServerSide;
            }
            if (binding.bDestroy === false) {
                options.bDestroy = binding.bDestroy;
            }
            ////////////////////////////////
            if (!binding.sourceType) {
                throw ('Source Type Propery Not Set!')
            }

            if (binding.autoBind && binding.sourceType && binding.sourceType == 'ajax' && binding.ajaxSource) {
                options.sAjaxSource = binding.ajaxSource();
            }

            if (binding.bAutoWidth) {
                options.bAutoWidth = binding.bAutoWidth;
            }
            //oLanguage parameters
            if (binding.oLanguage) {
                options.oLanguage = binding.oLanguage;
            }
            if (binding.sProcessing) {
                options.oLanguage.sProcessing = binding.sProcessing;
            }
            if (binding.sLengthMenu) {
                options.oLanguage.sLengthMenu = binding.sLengthMenu;
            }
            if (binding.sZeroRecords) {
                options.oLanguage.sZeroRecords = binding.sZeroRecords;
            }
            if (binding.sInfo) {
                options.oLanguage.sInfo = binding.sInfo;
            }
            if (binding.sInfoEmpty) {
                options.oLanguage.sInfoEmpty = binding.sInfoEmpty;
            }
            if (binding.sInfoFiltered) {
                options.oLanguage.sInfoFiltered = binding.sInfoFiltered;
            }
            if (binding.sInfoPostFix) {
                options.oLanguage.sInfoPostFix = binding.sInfoPostFix;
            }
            if (binding.sSearch) {
                options.oLanguage.sSearch = binding.sSearch;
            }
            if (binding.sUrl) {
                options.oLanguage.sUrl = binding.sUrl;
            }
            if (binding.oPaginate) {
                options.oLanguage.oPaginate = binding.oPaginate;
            }
            ////////////////////////////////
            if (binding.selectedCheckBoxColumn) {
                options.aoColumns = [];
                options.aoColumns.push(
                    {
                        "mData": 'selectedColumn', "bSearchable": false, "bSortable": false, "sWidth": "10px",
                        "mRender": function (data, type, full) {
                            return '<div style="width:100%" class="center"><label><input type="checkbox"><span class="lbl"></span></label></div>'
                        }
                    });
            }

            // Define the tables columns.
            //if (binding.columns && binding.columns.length) {
            //    options.aoColumns = [];
            //    ko.utils.arrayForEach(binding.columns, function (col) {
            //        options.aoColumns.push({
            //            mDataProp: col
            //        });
            //    })
            //}

            // Define column data attributes
            if (binding.columns && binding.columns.length) {
                if (!options.aoColumns) options.aoColumns = [];
                ko.utils.arrayForEach(binding.columns, function (col) {
                    //{"mData" : "status", "sWidth": "6%"},

                    options.aoColumns.push({
                        mDataProp: col.name
                    });

                    theIndex = binding.columns.indexOf(col);
                    //agar sotone checkbox active bashad sotoone aval marboot be an ast va baghiye sotonha az edame dar oaColumns add mishavad
                    if (binding.selectedCheckBoxColumn) { theIndex = theIndex + 1; }
                    if (col.dataSort) {
                        options.aoColumns[theIndex].iDataSort = col.dataSort;
                    }

                    if (col.sortType) {
                        options.aoColumns[theIndex].sType = col.sortType;
                    }

                    if (col.sortable == false) {
                        options.aoColumns[theIndex].bSortable = col.sortable;
                    }

                    if (col.visible == false) {
                        options.aoColumns[theIndex].bVisible = col.visible;
                    }

                    if (col.searchable == false) {
                        options.aoColumns[theIndex].bSearchable = col.searchable;
                    }

                    if (col.title) {
                        options.aoColumns[theIndex].title = col.title;
                    } else {
                        options.aoColumns[theIndex].title = '';
                    }
                })
            }

            if (binding.sortingFixed && binding.sortingFixed.length) {
                options.aaSortingFixed = [];
                ko.utils.arrayForEach(binding.sortingFixed, function (item) {
                    options.aaSortingFixed.push([item.column, item.direction]);
                })
            }

            if (binding.initialSortColumn) {
                options.aaSortingFixed = [[binding.initialSortColumn, 'asc']];
            }

            if (binding.autoWidth) {
                options.bAutoWidth = binding.autoWidth;
            } else {
                options.bAutoWidth = false;
            }

            if (binding.sDom) {
                options.sDom = binding.sDom;
            }

            if (binding.iDisplayLength) {
                options.iDisplayLength = binding.iDisplayLength;
            }

            if (binding.sPaginationType) {
                options.sPaginationType = binding.sPaginationType;
            }

            if (binding.bPaginate) {
                options.bPaginate = binding.bPaginate;
            }

            // Register the row template to be used with the DataTable.
            if (binding.rowTemplate && binding.rowTemplate != '') {
                options.fnRowCallback = function (row, data, displayIndex, displayIndexFull) {
                    // Render the row template for this row.
                    // BUGFIX N Beemster
                    // als dit gebruikt wordt, is de context niet beschikbaar in de binding van de template
                    //ko.renderTemplate(binding.rowTemplate, data, null, row, "replaceChildren");
                    return row;
                }

                // zo werkt het wel!
                options.fnCreatedRow = function (nRow, aData, iDataIndex) {
                    var destRow = $(nRow);
                    destRow.empty();
                    var dataSource = ko.utils.unwrapObservable(binding.dataSource);
                    var observableForThisRow = dataSource[iDataIndex];
                    var localContext = new ko.bindingContext(observableForThisRow, bindingContext);
                    ko.renderTemplate(binding.rowTemplate, localContext, null, nRow, "replaceChildren");
                }
            }

            if (binding.fnServerParams) {
                options.fnServerParams = function (aoData) {
                    //if necessary enter this info for retreive and use in apiController Method
                    //aoData.push({ name: "UserName", value: $('#txtUserName').val() });
                    //aoData.push({ name: "FirstName", value: $('#txtFirstName').val() });
                    //aoData.push({ name: "LastName", value: $('#txtLastName').val() });
                    //aoData.push({ name: "MelliCode", value: $('#txtMelliCode').val() });
                }
            }

            /////////////////////
            //if (binding.dataSource) {
            //    var dataSource = ko.utils.unwrapObservable(binding.dataSource);

            //    if (dataSource instanceof Array) {
            //        // Set the initial datasource of the table.
            //        options.aaData = ko.utils.unwrapObservable(binding.dataSource);

            //        // If the data source is a knockout observable array...
            //        if (ko.isObservable(binding.dataSource)) {
            //            // Subscribe to the dataSource observable.  This callback will fire whenever items are added to
            //            // and removed from the data source.
            //            binding.dataSource.subscribe(function (newItems) {
            //                // ** Redraw table **
            //                var dataTable = $(element).dataTable();
            //                // Get a list of rows in the DataTable.
            //                var tableNodes = dataTable.fnGetNodes();

            //                // If the table contains rows...
            //                if (tableNodes.length) {
            //                    // Unregister each of the table rows from knockout.
            //                    ko.utils.arrayForEach(tableNodes, function (node) {
            //                        ko.cleanNode(node);
            //                    });
            //                    // Clear the datatable of rows.
            //                    dataTable.fnClearTable();
            //                }

            //                // Unwrap the items in the data source if required.
            //                var unwrappedItems = [];
            //                ko.utils.arrayForEach(newItems, function (item) {
            //                    unwrappedItems.push(ko.utils.unwrapObservable(item));
            //                });

            //                // Add the new data back into the data table.
            //                dataTable.fnAddData(unwrappedItems);
            //            });
            //        }

            //    }
            //        // If the dataSource was not a function that retrieves data, or a javascript object array containing data.
            //    else {
            //        throw 'The dataSource defined must a javascript object array';
            //    }
            //}
            /////////////////////
            // If no fnRowCallback has been registered in the DataTable's options, then register the default fnRowCallback.
            // This default fnRowCallback function is called for every row in the data source.  The intention of this callback
            // is to build a table row that is bound it's associated record in the data source via knockout js.
            //if (!options.fnRowCallback) {
            //    options.fnRowCallback = function (row, srcData, displayIndex, displayIndexFull) {
            //        var columns = this.fnSettings().aoColumns

            //        // Empty the row that has been build by the DataTable of any child elements.
            //        var destRow = $(row);
            //        destRow.empty();

            //        // For each column in the data table...
            //        ko.utils.arrayForEach(columns, function (column) {
            //            var columnName = column.mDataProp;

            //            var newCell = $("<td></td>");

            //            // bind the cell to the observable in the current data row.
            //            var accesor = eval("srcData['" + columnName.replace(".", "']['") + "']");

            //            destRow.append(newCell);
            //            if (columnName == 'action') {
            //                ko.applyBindingsToNode(newCell[0], {
            //                    html: accesor
            //                }, srcData);
            //            } else {
            //                ko.applyBindingsToNode(newCell[0], {
            //                    text: accesor
            //                }, srcData);
            //            }
            //        });

            //        return destRow[0];
            //    }
            //}

            //// If no fnDrawCallback has been registered in the DataTable's options, then register the default here.
            //// This default callback is called every time the table is drawn (for example, when the pagination is clicked).
            //if (!options.fnDrawCallback) {
            //    options.fnDrawCallback = function () {
            //        // There are some assumptions here that need to be better abstracted
            //        $(binding.expandIcon).click(function () {
            //            var theRow = $(this).parent().parent()[0]; //defined by the relationship between the clickable expand icon and the row. assumes that the icon (the trigger) is in a td which is in a tr.
            //            rowContent = $(theRow).find(".hiddenRow").html();

            //            tableId = local[binding.gridId];

            //            if (tableId.fnIsOpen(theRow)) {
            //                $(this).removeClass('icon-contract ' + binding.expandIcon);
            //                $(this).addClass('icon-expand ' + binding.expandIcon);
            //                tableId.fnClose(theRow);
            //            } else {
            //                $(this).removeClass('icon-expand ' + binding.expandIcon);
            //                $(this).addClass('icon-contract ' + binding.expandIcon);
            //                tableId.fnOpen(theRow, rowContent, 'info_row');
            //            }
            //        });

            //        if (binding.tooltip) {
            //            if (binding.tooltip[0]) {
            //                // bootstrap tooltip definition
            //                $("[rel=" + binding.tooltip[1] + "]").tooltip({
            //                    placement: 'top',
            //                    trigger: 'hover',
            //                    animation: true,
            //                    delay: {
            //                        show: 1000,
            //                        hide: 10
            //                    }
            //                });
            //            }
            //        }
            //    }
            //}
            /////////////////////
            binding.refreshPage.subscribe(function (newItems) {
                //aval false mikonim ta badan vaghti true shod dobare varede method shavad
                //vaghti false mikardam dobare varede method mishod vase hamin sharte return ra gozashtim
                binding.selectedRowId(null);
                binding.refreshPage(false);
                if (newItems !== true) {
                    return;
                }
                //old
                if (binding.autoBind) {
                    var oTable = $(element).DataTable();
                    oTable.clear().draw();
                } else {
                    var oTable = $(element).DataTable();
                    oTable.destroy();

                    $(element).dataTable(options);
                    $.data(element, isInitialisedKey, true);
                    oTable.clear().draw();
                }
            });
            /////////////////////

            //agar yek bar datatable ra por mikard va bare dovom method ra farakhani mikardim varede .datatable() nemishod
            //baraye hamin an ra destroy kardim ke khali shavad bad dobare por mikonim
            //old
            if (binding.autoBind) {
                var oTable = $(element).DataTable();
                oTable.destroy();
                $(element).dataTable(options);
                $.data(element, isInitialisedKey, true);
            } else {
                options.bServerSide = false;
                var oTable = $(element).DataTable();
                oTable.destroy();
                $(element).dataTable(options);
                //$.data(element, isInitialisedKey, true);

                if (binding.sourceType && binding.sourceType == 'ajax' && binding.ajaxSource) {
                    options.sAjaxSource = binding.ajaxSource();
                    options.bServerSide = true;
                }
            }
            $(function () {
                //click rooye radife tr, checked combo an ra true mikonad
                //$('#dgvTemp').delegate('tbody > tr', 'click', function () {
                $(element).delegate('tbody > tr', 'click', function () {
                    $(this).find('input[name="groupRadio"]').prop('checked', true);
                });
            });

            // Tell knockout that the control rendered by this binding is capable of managing the binding of it's descendent elements.
            // This is crutial, otherwise knockout will attempt to rebind elements that have been printed by the row template.
            return {
                controlsDescendantBindings: true
            };
        }
        ///////////////////////////
        //, convertDataCriteria: function (srcOptions) {
        //    var getColIndex = function (name) {
        //        var matches = name.match("\\d+");

        //        if (matches && matches.length) return matches[0];

        //        return null;
        //    }

        //    var destOptions = {
        //        Columns: []
        //    };

        //    // Figure out how many columns in in the data table.
        //    for (var i = 0; i < srcOptions.length; i++) {
        //        if (srcOptions[i].name == "iColumns") {
        //            for (var j = 0; j < srcOptions[i].value; j++)
        //                destOptions.Columns.push(new Object());
        //            break;
        //        }
        //    }

        //    ko.utils.arrayForEach(srcOptions, function (item) {
        //        var colIndex = getColIndex(item.name);

        //        if (item.name == "iDisplayStart") destOptions.RecordsToSkip = item.value;
        //        else if (item.name == "iDisplayLength") destOptions.RecordsToTake = item.value;
        //        else if (item.name == "sSearch") destOptions.GlobalSearchText = item.value;
        //        else if (cog.string.startsWith(item.name, "bSearchable_")) destOptions.Columns[colIndex].IsSearchable = item.value;
        //        else if (cog.string.startsWith(item.name, "sSearch_")) destOptions.Columns[colIndex].SearchText = item.value;
        //        else if (cog.string.startsWith(item.name, "mDataProp_")) destOptions.Columns[colIndex].ColumnName = item.value;
        //        else if (cog.string.startsWith(item.name, "iSortCol_")) {
        //            destOptions.Columns[item.value].IsSorted = true;
        //            destOptions.Columns[item.value].SortOrder = colIndex;

        //            var sortOrder = ko.utils.arrayFilter(srcOptions, function (item) {
        //                return item.name == "sSortDir_" + colIndex;
        //            });

        //            if (sortOrder.length && sortOrder[0].value == "desc") destOptions.Columns[item.value].SortDirection = "Descending";
        //            else destOptions.Columns[item.value].SortDirection = "Ascending";
        //        }
        //    });

        //    return destOptions;
        //}
        ///////////////////////////
    };
    //ko.bindingHandlers.dialog = {
    //    init: function (element, valueAccessor, allBinding, viewModel, bindingContext) {
    //
    //        $.ajax({
    //            type: "Get",
    //            url: '/PersonShell/Create',
    //            data: null,
    //            dataType: "html",
    //        }).success(function (data) {
    //            //ShowMyDialog(data, new app.enumerations.action().create, false);
    //            mainAction = new app.enumerations.action().create;
    //
    //            var dialogTitle = new app.enumerations.action().toString(mainAction);

    //            BootstrapDialog.show({
    //                id: "thisDialog",
    //                type: BootstrapDialog.TYPE_PRIMARY,
    //                title: dialogTitle,
    //                message: function (dialogRef) {
    //                },
    //                onshow: function (dialogRef) {
    //                    ///////////////////////////////////////////////
    //                    dialogRef.getModalBody().html(data);
    //                    //dialogRef.data = '<div data-bind="text: crudname">';
    //                    //dialogRef.getModalBody().html('<div data-bind="text: crudname">');
    //                    ///////////////////////////////////////////////

    //                    //dialogRef.setClosable(!lockScreenBool);
    //                    //'<div data-bind="value: "' + self.crudname +' >'
    //                },
    //                onshown: function (dialogRef) {
    //                    if (mainAction == new app.enumerations.action().read) {
    //                        (dialogRef).getButton('acceptButton').disable().css("visibility", "hidden");
    //                    }
    //                },
    //                onhide: function (dialogRef) {
    //                },
    //                onhidden: function (dialogRef) {
    //                    mainID = 0;
    //                },
    //                buttons: [
    //                    {
    //                        id: 'acceptButton',
    //                        label: ' تایید ',
    //                        cssClass: 'btn-primary btn-small ' + (mainAction == new app.enumerations.action().read ? ' hidden ' : ''),
    //                        //cssClass: 'btn-primary btn-small hidden',
    //                        icon: 'fa fa-check ',
    //                        spinicon: 'icon-asterisk',
    //                        action: function (dialog) {
    //                            DialogOperation.save().then(function (result) {
    //                                if (result) {
    //                                    dialog.close();
    //                                    refreshDataTable();
    //                                }
    //                            }, function (e) {
    //                                //Error
    //                                //alert(e.message);
    //                            });
    //                        }
    //                    },
    //                {
    //                    icon: 'fa fa-times',
    //                    label: ' خروج ',
    //                    cssClass: 'btn-default btn-small',
    //                    action: function (dialog) {
    //                        dialog.close();
    //                    }
    //                }]
    //            });

    //            //ShowMyDialog(data, new app.enumerations.actions.action().add, false);
    //            //ShowMyDialog(data, new action().add, false);
    //        }).error(function (e) {
    //            alert(e.error);
    //        }).done(function (e) {
    //        });
    //    }
    //};
    //ko.components.register("todo", { require: 'components/todo/todo' });
    //ko.bindingHandlers.blurOnEnter = {
    //    init: function (element, valueAccessor) {
    //        $(element).keypress(function (event) {
    //            var value = ko.unwrap(valueAccessor());
    //            if (event.keyCode == 13 && value) {
    //                event.preventDefault();
    //                $(element).trigger('blur');
    //            }
    //        });
    //    }
    //};
});