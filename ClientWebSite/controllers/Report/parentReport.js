angular.module("module").controller("parentReport", ["$scope", "getAjax", "$http", "$location", "$routeParams",

    function($scope, getAjax, $http, $location, $routeParams) {
        console.log("enter parentReporthhhhhhhhhhhhhhhhh");
        $scope.datenow = "";
        $scope.total = 0;
        $scope.childId = $routeParams.childId;

        getAjax.response("http://localhost:9408/api/ParentReport/GetPatentReport?childId=" + $scope.childId + "&specificDate=" + $location.search().date)
            .then((data) => {
                console.log("get data from ajax request", data.data);
                $scope.content = data.data;
                $scope.namechild = $scope.content[0].childName;
                $scope.month = $location.search().date;
                for (var i = 0; i < $scope.content.length; i++) {
                    $scope.total += $scope.content[i].amount;
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
                            url: "http://localhost:9408/api/ReportParent?childId=" + $scope.childId,
                            data: { Value: s },
                            header: { 'Content-Type': 'application/json; charset=utf-8' }
                        });
                    })

                }
            });
        }

        $scope.addSignature = () => {
            console.log("ghhhhh");
            $scope.showSignature = true;
        }

        $scope.saveSignature = () => {
            $scope.showSignature = false;
        }

    }
]);