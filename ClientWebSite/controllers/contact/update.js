angular.module("module").controller("updateContact", ["$scope", "getAjax", "$http", "$location", "$routeParams", "serverUrl",
    function($scope, getAjax, $http, $location, $routeParams, serverUrl) {
        $scope.isLoading = true;

        console.log("hello from update contact")
        $scope.id = $routeParams.id;
        console.log($scope.id);
        $scope.ParentId = "";
        getAjax.response(serverUrl + "api/Contacts/GetById?id=" + $scope.id).
        then(function(data) {
            $scope.number = data.data.number;
            $scope.contactName = data.data.contactName;
            $scope.contactType = data.data.contactType;
            $scope.ParentId = data.data.parentId;
            $("#contactType").val(data.data.contactType);
            $scope.isLoading = false;

        }, function(error) {
            console.log("error");
        });
        $scope.sendData = function() {
            var data = {
                id: $scope.id,
                Number: $scope.number,
                ContactName: $scope.contactName,
                ContactType: $("#contactType").val(),
                ParentId: $scope.ParentId
            };
            console.log(data);
            $http.put(serverUrl + "api/Contacts/" + $scope.id, data).
            then((data) => {
                    console.log(data);
                    $location.path("/contacts/" + $scope.ParentId);
                },
                (error) => {
                    console.log(error);
                });
        }
        $scope.back = () => {
            $location.path("/contacts/" + $scope.ParentId);
        }
    }
]);