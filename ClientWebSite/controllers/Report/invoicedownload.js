angular.module("module").controller("invoicedownload", ["$scope", "getAjax", "$http", "$location", "$routeParams",
    function($scope, getAjax, $http, $location, $routeParams) {
        console.log("enter final report");
        $scope.total = 0;
        $scope.childId = $routeParams.childId;
        $scope.mail = "";
        $scope.signaturePerson = "Niveen Zaki"

        getAjax.response("http://localhost:9408/api/Invoice?childId=" + $scope.childId + "&specificDate=" + $location.search().date).
        then(function(data) {
            console.log(data.data)
            $scope.content = data.data;
            $scope.invoiceNumber = $scope.content[0].invoiceNumber;
            $scope.invoiceDate = $scope.content[0].invoiceDate;
            $scope.billingNumber = $scope.content[0].billingNumber;
            $scope.childName = $scope.content[0].childName;
            $scope.lastName = $scope.content[0].lastName;
            $scope.month = $location.search().date;
            for (var i = 0; i < data.data.length; i++) {
                $scope.total += $scope.content[i].totalAmount;
                console.log($scope.total)
            }
        }, function(error) {
            console.log("error");
        });


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
                            url: "http://localhost:9408/api/Invoice?childId=" + $scope.childId + "&email=" + $scope.mail,
                            data: { Value: s },
                            header: { 'Content-Type': 'application/json; charset=utf-8' }
                        });
                    })

                }
            });
            $scope.displayMail = false;
        }

        $scope.addSignature = () => {
            console.log("ghhhhh");
            $scope.showSignature = true;
        }

        $scope.saveSignature = () => {
            $scope.showSignature = false;
        }
        $scope.enterMail = () => {
            $scope.displayMail = true;
        }
    }
]);