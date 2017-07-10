angular.module("module").controller("invoiceReport", ["$scope", "getAjax", "$http", "$location",
    function($scope, getAjax, $http, $location) {
        console.log("enter invoiceReport");
        $scope.datenow = "";
        $scope.showSelect = false;


        $scope.showCheckbox = () => {
            $scope.showSelect = true;
        }


        $('select').on('change', function() {
            $scope.fundType = $(this).find(":selected").val();
            getAjax.response("http://localhost:9408/api/Invoice?specificDate=" + $scope.datenow + "&fundType=" + $scope.fundType)
                .then((data) => {
                    $scope.content = data.data;
                }, (error) => {
                    console.log(error);
                })
        })


        $scope.generateInvoice = (childId) => {
            console.log("enter generateInvoice fuction ", childId);
            console.log($scope.fundType == 'AFU');
            if ($scope.fundType == 'AFU') {
                console.log("enter Afu event")
                $location.path("/invoicedownload/" + childId).search({ date: $scope.datenow });
            } else if ($scope.fundType == "HomeFunding") {
                console.log("enter homefund event")
                $location.path("/homeFundInvoice/" + childId).search({ date: $scope.datenow });
            } else {
                console.log("enter other fund event")
                $location.path("/generalInvoice/" + childId + "/" + $scope.fundType).search({ date: $scope.datenow });
            }
        }

        $scope.generateParentReport = (childId) => {
            console.log("parent reort");
            $location.path("/parentReport/" + childId).search({ date: $scope.datenow });



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