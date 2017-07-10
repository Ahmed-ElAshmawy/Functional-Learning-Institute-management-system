angular.module("module").controller("createparent", ["$scope", "getAjax", "$http", "$location", "postAjax", "serverUrl",
    function($scope, getAjax, $http, $location, postAjax, serverUrl) {

        $scope.FName = $scope.MName = $scope.Em = $scope.BD = $scope.PC = $scope.SNo = $scope.SNa = $scope.Pro = $scope.Ci = $scope.CCNa = $scope.CCNo = $scope.ECNa = $scope.ECNo = "";
        $scope.reset = (createForm) => {
            createForm.$setPristine();
            $scope.FName = $scope.MName = $scope.Em = $scope.BD = $scope.PC = $scope.SNo = $scope.SNa = $scope.Pro = $scope.Ci = $scope.CCNa = $scope.CCNo = $scope.ECNa = $scope.ECNo = "";
        }
        $scope.add = () => {
            postAjax.post(serverUrl + "api/Parents/", $("#createForm").serialize()).then((response) => {
                console.log(response.data);
                $location.path("/parent/");
            }, (error) => { console.log(error.statusText) });;
        }
        $scope.back = () => { $location.path("/parent/"); }
    }
]);