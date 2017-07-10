angular.module("module").controller("updateparentemail", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {
        $scope.email = $location.search().parentEmail;
        $scope.parentId = $location.search().parentId
        console.log("parentid", $scope.parentId)
        $scope.update = () => {
            updateObj = {
                Email: $("#updatedEmail").val(),
                ParentId: $location.search().parentId,
                Id: $location.search().Id
            };
            $http({
                    url: serverUrl + "api/ParentEmail/" + $location.search().Id,
                    method: "PUT",
                    data: updateObj,
                    header: { 'Content-Type': 'application/x-www-form-urlencoded' }
                })
                .then((response) => {
                    console.log(response.data);
                    $location.path('/parentemail/' + $scope.parentId)
                }, (error) => {
                    console.log(error.statusText)
                });
        }

        $scope.back = () => {
            $location.path('/parentemail/' + $scope.parentId)
        }
    }
])