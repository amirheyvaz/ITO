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
    buildReport(fromYear, ToYear, data);
}
function createChart(fromYear, ToYear, data) {
    var from = fromYear, to = ToYear;
    while (from <= to) {
        categories.push(from);
        from++;
    }
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
    
}
function CreateKendoChart() {
    var chartWidth = document.getElementById("chartContainer").offsetWidth;
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
        seriesDefaults: {
            type: "line",
            style: "smooth"
        },
        series:series,
        valueAxis: {
            labels: {
                format: "{0}"
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