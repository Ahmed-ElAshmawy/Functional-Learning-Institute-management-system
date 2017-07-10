angular.module("module").controller("therapistReportView", ["$scope", "getAjax", "$http", "$location",
    function($scope, getAjax, $http, $location) {
        console.log("enter invoiceReport");
        $scope.datenow = "";

        $scope.showCheckbox = () => {
            console.log("chosen date");
            getAjax.response("http://localhost:9408/api/ReportTherapist?specificDate=" + $scope.datenow)
                .then((data) => {
                    //console.log("get data from ajax request", data.data);
                    $scope.therapistData = data.data;
                    console.log($scope.therapistData);
                }, (error) => {
                    console.log(error);
                })
        }


        $scope.biMonthReport = (therapistId) => {
            console.log("event 1")
            console.log("id", therapistId)
            console.log("date", $scope.datenow)
            $location.path("/biMonthReport/" + therapistId).search({ date: $scope.datenow });
        }

        $scope.endMonthReport = (therapistId) => {
            console.log("event 2")
            $location.path("/endMonthReport/" + therapistId).search({ date: $scope.datenow });
        }
        $scope.taskReport = (therapistId) => {
            console.log("event 3")
            $location.path("/finalMonthReport/" + therapistId).search({ date: $scope.datenow });
        }

        $('#datePick').datepicker({
            format: "mm/yyyy",
            startView: 1,
            minViewMode: 1,
            maxViewMode: 2,
            autoclose: true,
            todayHighlight: true,
            toggleActive: true
        });




    }
]);