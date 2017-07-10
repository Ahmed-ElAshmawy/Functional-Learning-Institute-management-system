angular.module("module").controller("forgetPassword", ["$scope", "getAjax", "serverUrl",
    function($scope, getAjax, serverUrl) {
        console.log("Hello From forgetPassword");
        $scope.forgetPassword = () => {
            getAjax.response(serverUrl + "api/Account/ForgotPassword?username=" + $scope.userName).then(
                (res) => {
                    $scope.resetOk = "Please check email address you will find message with instruction to reset password";
                    $scope.resetError = "";
                },
                (error) => {
                    $scope.resetOk = "";
                    $scope.resetError = error.statusText;
                }
            )
        }
    }
]);