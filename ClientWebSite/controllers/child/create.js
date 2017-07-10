angular.module("module").controller("createchild", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {
        var id = $location.search().Id;
        $scope.FName = $scope.MName = $scope.Em = $scope.BD = $scope.PC = $scope.SNo = $scope.SNa = $scope.Pro = $scope.Ci = $scope.CCNa = $scope.CCNo = $scope.ECNa = $scope.ECNo = "";
        $scope.add = () => {
            var crObj = $("#createForm").serialize();
            crObj += "&parentId=" + id;
            getAjax.postForm(serverUrl + "api/Children/", crObj)
                .then((response) => {
                    console.log(response.data);
                    $location.path("/child")
                }, (error) => { console.log(error.statusText) });
        }
        $scope.reset = (createForm) => {
            createForm.$setPristine();
            $scope.FName = $scope.MName = $scope.Em = $scope.BD = $scope.PC = $scope.SNo = $scope.SNa = $scope.Pro = $scope.Ci = $scope.CCNa = $scope.CCNo = $scope.ECNa = $scope.ECNo = "";
        }
        $scope.back = () => { $location.path("/child") }
    }
]);