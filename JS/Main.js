var people = [], popupNotification, categories = [], series = [];
$(document).ready(function () {
    getPeople();
    buildWindows();
});
function buildWindows() {
    popupNotification = $("#popupNotification").kendoNotification().data("kendoNotification");
    progressWindow = $("#progressWindow")
              .kendoWindow({
                  modal: true,
                  visible: false,
                  scrollable: false,
                  height: 300,
                  width: 300,
                  actions: {}
              }).data("kendoWindow");
}
$(document).bind("kendo:skinChange", createChart);
function getPeople(){
    $.ajax({
        type: "POST",
        url: "Report.asmx/getAllUsers",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            people = response.d;
            buildMultiSelect();
        },
        error: function () {
            alert_kendo("Can't conect to web service", "error");
        }
    });
}
function makeChanges() {
    var fromYear = document.getElementById("fromYear").value;
    var ToYear = document.getElementById("ToYear").value;
    // get a reference to the list box widget
    var listBox = $("#selected").data("kendoListBox");
    // selects first list box item
    var data = listBox.dataItems();
    debugger;
    buildReport(fromYear, ToYear, data);
}
function createChart(fromYear, ToYear, data) {
    var from = fromYear, to = ToYear;
    while (from <= to) {
        categories.push(from);
        from++;
    }
    var chartWidth = document.getElementById("chartContainer").offsetWidth;
    var jsonData = JSON.stringify({ users: data, fromYear: fromYear, toYear: ToYear });
    $.ajax({
        type: "POST",
        url: "Report.asmx/getDots",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsonData,
        success: function (response) {
            series = response.d;
            CreateKendoChart();
        },
        error: function () {
            alert_kendo("Can't conect to web service", "error");
        }
    });
    //categories = [2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011];
    //series = [{
    //    name: "علی",
    //    data: [3.907, 7.943, 7.848, 9.284, 9.263, 9.801, 3.890, 8.238, 9.552, 6.855]
    //}, {
    //    name: "اکبر",
    //    data: [1.988, 2.733, 3.994, 3.464, 4.001, 3.939, 1.333, -2.245, 4.339, 2.727]
    //}, {
    //    name: "صادق",
    //    data: [4.743, 7.295, 7.175, 6.376, 8.153, 8.535, 5.247, -7.832, 4.3, 4.3]
    //}, {
    //    name: "سامان",
    //    data: [-0.253, 0.362, -3.519, 1.799, 2.252, 3.343, 0.843, 2.877, -5.416, 5.590]
    //}];
    
}
function CreateKendoChart() {
    $("#chart").kendoChart({
        title: {
            text: "نمودار تغییرات حساب افراد"
        },
        legend: {
            position: "bottom"
        },
        chartArea: {
            background: "",
            width: chartWidth
        },
        zoomable: true,
        seriesDefaults: {
            type: "line",
            style: "smooth"
        },
        series:series,
        valueAxis: {
            labels: {
                format: "{0}%"
            },
            line: {
                visible: false
            },
            axisCrossingValue: -10
        },
        categoryAxis: {
            categories: categories,
            majorGridLines: {
                visible: false
            },
            labels: {
                rotation: "auto"
            }
        },
        tooltip: {
            visible: true,
            format: "{0}%",
            template: "#= series.name #: #= value #"
        }
    });
}
function buildReport(fromYear, ToYear, data) {

    //show
    document.getElementById("reports_div").style.display = "block";
    createChart(fromYear, ToYear, data);
}
function buildMultiSelect() {
    $("#people_picker").kendoListBox({
        connectWith: "selected",
        toolbar: {
            tools: ["transferTo", "transferFrom", "transferAllTo", "transferAllFrom"]
        },
        dataTextField: "fullName",
        dataValueField: "fullName",
        dataSource: people,
        selectable: "multiple"
    });
    $("#selected").kendoListBox({
        connectWith: "people_picker",
        dataTextField: "fullName",
        dataValueField: "fullName",
        selectable: "multiple"
    });
}
function alert_kendo(massage, kind) {
    popupNotification.show(massage, kind);
}