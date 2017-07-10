angular.module("module").controller("therapistTask", ["$scope", "getAjax", "$http", "$location", function($scope, getAjax, $http, $location) {
    console.log("Hello From therapist Task");
    var therapistId = $location.search().therapistId;

    $scope.getdata = () => {
        getAjax.response("http://localhost:9408/api/TherapistTasks/" + therapistId).
        then((response) => {
            console.log(response.data);
            $scope.theraTask = response.data;
            console.log($scope.theraTask);
            // $scope.description= response.data.description;
            //console.log("th task", $scope.theraTask)
        }, (error) => { console.log(error.statusText) });
    }
    $scope.getdata();


    $scope.deletetask = (id) => {
        getAjax.response("http://localhost:9408/api/TherapistTasks/DeleteTherapistTask?id=" + id).
        then((response) => {

            console.log("task deleted")
        }, (error) => { console.log(error.statusText) });
    }

    $scope.updatetask = (id) => {
        console.log(id)
        $location.path("/updateTask/").search({
            TaskId: id,
            therapistId: therapistId
        });
    }



    $scope.createTask = () => {

        $location.path("/createTask/").search({
            therapistId: therapistId
        });
    }






}]);