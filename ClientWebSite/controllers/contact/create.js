angular.module("module").controller("createContact", ["$scope", "getAjax", "$http", "$location", "$routeParams", "serverUrl",
    function($scope, getAjax, $http, $location, $routeParams, serverUrl) {
        console.log(serverUrl);
        console.log("hello from create contact")
        $scope.id = $routeParams.id; //parentid
        $scope.sendData = function() {
            var data = {
                Number: $scope.number,
                ContactName: $scope.contactName,
                ContactType: $("#contactType").val(),
                ParentId: $scope.id
            };
            console.log(data);
            $http.post(serverUrl + "api/Contacts", data).
            then((data) => {
                    console.log(data);
                    $location.path("/contacts/" + $scope.id);
                },
                (error) => {
                    console.log(error);
                });
        }
        $scope.back = () => {
            $location.path("/contacts/" + $scope.id);
        }
        $scope.reset = () => {
            $scope.number = $scope.contactName = $scope.ContactType = "";
            $scope.error = "";
        }
    }
]);