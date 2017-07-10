angular.module("module").controller("createTask", ["$scope", "getAjax", "$http", "$location", "$routeParams", "serverUrl",
    function($scope, getAjax, $http, $location, $routeParams, serverUrl) {

        var therapistId = $location.search().therapistId;

        $scope.FName = $scope.Em = $scope.BD = $scope.BD = $scope.PC = $scope.SNo = $scope.SNa = $scope.Pro = $scope.Ci = $scope.CCNa = $scope.CCNo = $scope.ECNa = $scope.ECNo = "";

        $scope.addtask = () => {
            console.log("crete");
            var crObj = $("#createForm").serialize();
            crObj += "&TherapistId=" + therapistId;

            console.log(crObj);
            $http.post(serverUrl + "api/TherapistTasks/", crObj, {
                headers: {
                    'content-type': 'application/x-www-form-urlencoded'
                }
            }).then((response) => {
                console.log("created");
                $location.path("/therapistTask")
            }, (error) => { console.log(error.statusText) });
        }

        // $scope.addtask = () => {

        //      var crObj = $("#createForm").serialize();
        //      crObj += "&TherapistId=" + therapistId;
        //      console.log(crObj);
        //     console.log($scope.BD);
        //     $http({
        //         method: 'Post',
        //         url: 'http://localhost:9408/api/TherapistTasks/',
        //         data: crObj
        //     }).then(function successCallback(response) {
        //         console.log("created"); $location.path("/therapistTask")
        //     }, (error) => { console.log(error.statusText) });

        // }
        $scope.back = () => { $location.path("/therapistTask") }

    }
]);