angular.module("module").controller("resetpassword", ["$scope", "getAjax", "$location", "serverUrl", "$timeout",
    function($scope, getAjax, $location, serverUrl, $timeout) {

        console.log('resr');
        $scope.username = $location.search().username;
        $scope.token = $location.search().token;
        $scope.reset = () => {
            var data = {
                newPassword: $scope.password,
                confirmPassword: $scope.confirmPassword,
                code: $scope.token.split(' ').join('+'),
                username: $scope.username
            }
            getAjax.post(serverUrl + "api/Account/RecetPassword", data).then(
                (res) => {
                    $location.path("/login");
                },
                (error) => {
                    $scope.resetError = "Your restore key has been expired you will be redirected to Forget password form to request another key";

                    $timeout(function() {
                        $location.path("/forgetPassword");
                    }, 10000);
                }
            )
            console.log(data);
        }
    }
]);