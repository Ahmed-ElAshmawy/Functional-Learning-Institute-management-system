angular.module("module").controller("login", ["$scope", "$location", "postAjax", "auth", "getAjax", "serverUrl",
    function($scope, $location, postAjax, auth, getAjax, serverUrl) {
        console.log("Hello From Login");
        $scope.userName = "";
        $scope.password = "";
        $scope.signIn = () => {
            var data = {
                userName: $scope.userName,
                password: $scope.password
            }
            postAjax.login(serverUrl + "Token", data).
            then(function(res) {
                //get roles GetRole
                var credential = res.data;
                auth.set(res.data)
                console.log(res);
                //console.log(res.data);
                getAjax.get(serverUrl + "api/Account/GetRole").then(
                    (res) => {
                        //auth.setRoles(res.data.roles)
                        //auth.setUserId(res.data.id)
                        auth.setAll(credential, res.data.roles, res.data.id)
                        $location.path("/index");
                    },
                    (error) => {
                        console.log('Get roles error');
                    }
                )
            }, function(error) {
                console.log(error.data.error_description);
                $scope.loginError = error.data.error_description;
            });
        }
        $scope.forgetPassword = () => {
            $location.path("/forgetPassword");
        }
        $scope.getRoles = () => {
            //console.log(auth.getCredential());
            getAjax.response(serverUrl + "api/Account/ForgotPassword?username=parent1").then(
                (res) => {
                    console.log(res.data);
                },
                (error) => {
                    console.log('Get roles error');
                }
            )
        }
    }
]);