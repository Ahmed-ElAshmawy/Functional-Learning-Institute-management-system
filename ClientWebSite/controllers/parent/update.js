angular.module("module").controller("updateparent", ["$scope", "getAjax", "$http", "$location", "$window", "$filter", "$routeParams", "serverUrl",
    function($scope, getAjax, $http, $location, $window, $filter, $routeParams, serverUrl) {
        $scope.id = $routeParams.id;
        $scope.obj = "";
        $scope.show = false;
        getAjax.response(serverUrl + "api/Parents/" + $scope.id).
        then((response) => {
            $scope.obj = response.data;
            $scope.originalProduct = angular.copy($scope.obj);
            $scope.obj.birthDate = new Date($filter('date')($scope.obj.birthDate, "yyyy-MM-dd"));
            $scope.show = true;
        }, (error) => {
            console.log(error.statusText)
        });

        $scope.cancel = (updateForm) => {
            updateForm.$setPristine();
            $scope.obj = angular.copy($scope.originalProduct);
            $scope.obj.birthDate = new Date($filter('date')($scope.obj.birthDate, "yyyy-MM-dd"));
        }
        $scope.update = () => {
            var upObj = $("#updateForm").serialize();
            upObj += "&id=" + $scope.obj.id;
            $http.put(serverUrl + "api/Parents/" + $scope.obj.id, upObj, {
                headers: {
                    'content-type': 'application/x-www-form-urlencoded'
                }
            }).then((response) => {
                console.log(response.data);
                $location.path("/parent/");
            }, (error) => {
                console.log(error.statusText)
            });;
        }
        $scope.back = () => {
            $location.path("/parent/");
        }
    }
]);