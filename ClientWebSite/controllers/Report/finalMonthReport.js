angular.module("module").controller("finalMonthReport", ["$scope", "getAjax", "$http", "$location", "$routeParams",
    function($scope, getAjax, $http, $location, $routeParams) {
        console.log("enter home fund  report");

        $scope.total1 = 0;
        $scope.total2 = 0;
        $scope.therapistId = $routeParams.therapistId;
        console.log("search location", $location.search().date);

        getAjax.response("http://localhost:9408/api/ReportTherapist/FinalReport?therapistId=" + $scope.therapistId + "&spescificDate=" + $location.search().date)
            .then((data) => {
                console.log("final month report", data.data);
                $scope.content1 = data.data;
                $scope.name = $scope.content1[0].therapistName;
                //$scope.email = $scope.content1[0].email;
                for (var i = 0; i < data.data.length; i++) {
                    $scope.total1 += $scope.content1[i].amount;
                }
                $scope.month = $location.search().date;

                getAjax.response("http://localhost:9408/api/ReportTherapist/TaskReport?therapistId=" + $scope.therapistId + "&specificDate=" + $location.search().date)
                    .then((data) => {
                        console.log("final month report", data.data);
                        $scope.contentTask = data.data;
                        for (var i = 0; i < $scope.contentTask.length; i++) {
                            $scope.total2 += $scope.contentTask[i].amount;
                        }
                        $scope.month = $location.search().date;
                        $scope.total = $scope.total1 + $scope.total2;
                    }, (error) => {
                        console.log(error);
                    })
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