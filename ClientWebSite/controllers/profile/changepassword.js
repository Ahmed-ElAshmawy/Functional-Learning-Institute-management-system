angular.module("module").controller("changepassword", ["$scope", "getAjax", "serverUrl", "postAjax", "$location",
    function($scope, getAjax, serverUrl, postAjax, $location) {
        console.log("Hello From changepassword");
        $scope.error = false;
        $scope.submit = () => {
            postAjax.post(serverUrl + "api/Account/ChangePassword", $("#changepassform").serialize()).then((res) => {
                $location.path("/home");
                $scope.error = false;
            }, (error) => { $scope.error = true; })
        }

    }
]);