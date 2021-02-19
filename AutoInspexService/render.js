$(document).ready(function () {

    var myWindow = $("#window").kendoWindow({
        modal: true,
        visible: false,
        resizable: false,
        width: "1024px",
        height:"800px",
        title: "Camera Live View",
    });


    var grid = $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: "http://192.168.0.11/AutoPix/api/PICameras"
            },
            pageSize: 20
        },
        height: 550,
        toolbar: ["search"],
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [{
            field: "AutoInspexID",
            title: "AutoInspex ID",
        }, {
            field: "InstallerName",
            title: "Installer Name"
        }, {
            field: "RingPosition",
            title: "Ring Position"
        }, {
            field: "InstallDate",
            title: "Install Date"
        }, {
            field: "SerialNumber",
            title: "Serial Number"
        }, {
            field: "HousingID",
            title: "Housing ID"
        }, {
            field: "LensID",
            title: "Lens ID"
        }, {
            field: "SensorID",
            title: "Sensor ID"
        }, {
            field: "PiVersion",
            title: "PI Version"
        }, {
            field: "OS_ID",
            title: "OS ID"
        }, {
            field: "IPAddress",
            title: "IP Address"
        }, {
            field: "Status",
            title: "Status"
        }, {
            field: "TimeStamp",
            title: "TimeStamp"
        }, { command: { text: "Live Show", name: "ShowCamera", click: onEdit }, title: " ", width: "140px" }]
    });
   
    function onEdit(e) {
        e.preventDefault();
        var tr = $(e.target).closest("tr"); 
        var data = this.dataItem(tr);
        console.log("Details for: " + data.IPAddress);
        var ip = "http://"+data.IPAddress+":8000";
        myWindow.data("kendoWindow").title("Live Camera");
        myWindow.data("kendoWindow").refresh(ip);
        console.log(myWindow.data("kendoWindow").content);
        myWindow.data("kendoWindow").open().center();
    }
});