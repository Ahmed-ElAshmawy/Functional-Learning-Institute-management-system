angular.module("module").controller("updatechild", ["$scope", "getAjax", "$http", "$location", "$window", "$filter", "$routeParams", "serverUrl",
    function($scope, getAjax, $http, $location, $window, $filter, $routeParams, serverUrl) {
        var id = $location.search().Id;
        var cid = $location.search().cId;

        getAjax.response(serverUrl + "api/Children/" + cid).
        then((response) => {
            $scope.obj = response.data;
            console.log(response.data);
            $scope.originalProduct = angular.copy($scope.obj);
            $scope.obj.birthDate = new Date($filter('date')($scope.obj.birthDate, "yyyy-MM-dd"));
            $scope.obj.clientSince = new Date($filter('date')($scope.obj.clientSince, "yyyy-MM-dd"));
            $scope.obj.startTime = new Date($filter('date')($scope.obj.startTime, "yyyy-MM-dd"));
            $scope.obj.endTime = new Date($filter('date')($scope.obj.endTime, "yyyy-MM-dd"));
            $('#fundgroup').val($scope.obj.fundGroup)
        }, (error) => { console.log(error.statusText) });

        $scope.updatechild = () => {
            var upObj = $("#updateform").serialize();
            upObj += "&parentId=" + id;
            $http.put(serverUrl + "api/Children/" + cid, upObj, {
                headers: {
                    'content-type': 'application/x-www-form-urlencoded'
                }
            }).then((response) => {
                console.log(response.data);
                $location.path("/child")
            }, (error) => { console.log(error.statusText) });
        }

        $scope.cancel = (updateForm) => {
            updateForm.$setPristine();
            $scope.obj = angular.copy($scope.originalProduct);
            $scope.obj.birthDate = new Date($filter('date')($scope.obj.birthDate, "yyyy-MM-dd"));
            $scope.obj.clientSince = new Date($filter('date')($scope.obj.clientSince, "yyyy-MM-dd"));
        }
        $scope.back = () => { $location.path("/child") }
    }
]);