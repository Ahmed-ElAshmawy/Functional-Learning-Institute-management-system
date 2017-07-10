angular.module("module").controller("createparentemail", ["$scope", "getAjax", "$http", "$location", "serverUrl", "$routeParams",
    function($scope, getAjax, $http, $location, serverUrl, $routeParams) {
        // $scope.email = $('#parentemail').val()
        $scope.parentId = $routeParams.id
        $scope.add = () => {
            createObj = {
                ParentId: $scope.parentId,
                Email: $scope.email
            }
            console.log(createObj)
            $http({
                    url: serverUrl + "api/ParentEmail/",
                    method: "POST",
                    data: createObj,
                    header: { 'Content-Type': 'application/x-www-form-urlencoded' }
                })
                .then((response) => {
                    console.log(response.data);
                    $location.path('/parentemail/' + $scope.parentId)
                }, (error) => {
                    console.log(error.statusText)
                });
            $scope.showCreate = false;
        }

        $scope.reset = () => {
            $scope.email = "";
            $scope.error = "";
        }
        $scope.back = () => {
            $location.path('/parentemail/' + $scope.parentId)
        }

    }
])