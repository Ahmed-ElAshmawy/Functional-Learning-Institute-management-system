angular.module("module").controller("endMonthReport", ["$scope", "getAjax", "$http", "$location", "$routeParams",
    function($scope, getAjax, $http, $location, $routeParams) {
        console.log("enter home fund  report");
        $scope.total = 0;
        $scope.therapistId = $routeParams.therapistId;
        $scope.month = $location.search().date;
        $scope.reportType = "End"

        getAjax.response("http://localhost:9408/api/ReportTherapist/BiMonthReport?therapistId=" + $scope.therapistId + "&specificDate=" + $scope.month + "&reportType=" + $scope.reportType)
            .then((data) => {
                console.log("end month report", data.data);
                $scope.contentEnd = data.data;
                $scope.contentEnd = data.data;
                $scope.name = $scope.contentEnd[0].therapistName;
                console.log($scope.name);
                for (var i = 0; i < data.data.length; i++) {
                    $scope.total += $scope.contentEnd[i].amount;
                }
            }, (error) => {
                console.log(error);
            })


        $scope.download = function() {
            html2canvas(document.getElementById('parentReport'), {
                onrendered: function(canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 550,
                        }],
                        pageSize: 'A4',
                        compress: false
                    };
                    var r = pdfMake.createPdf(docDefinition);
                    console.log(pdfMake.createPdf(docDefinition).download("Invoce" + $scope.childName + ".pdf"));
                }
            });
        }


        $scope.sendFile = function() {
            console.log("enter send image buuton");
            html2canvas(document.getElementById('parentReport'), {
                onrendered: function(canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 550,
                        }],
                        pageSize: 'A4',
                        compress: false
                    };
                    var r = pdfMake.createPdf(docDefinition)
                    r.getDataUrl((s, d) => {
                        $http({
                            method: "POST",
                            url: "http://localhost:9408/api/ReportTherapist/SendMail?therapistId=" + $scope.therapistId,
                            data: { Value: s },
                            header: { 'Content-Type': 'application/json; charset=utf-8' }
                        });
                    })

                }
            });
        }
    }
]);