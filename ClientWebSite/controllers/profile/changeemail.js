angular.module("module").controller("changeemail", ["$scope", "getAjax", "serverUrl", "postAjax", "$location",
    function($scope, getAjax, serverUrl, postAjax, $location) {
        console.log("Hello From changeemail");
        $scope.error = false;
        $scope.submit = () => {
            postAjax.post(serverUrl + "api/Account/ChangeEmail", $("#changeemailform").serialize()).then((res) => {
                $location.path("/home");
                $scope.error = false;
            }, (error) => {
                $scope.error = true;
            });
        }
    }
]);