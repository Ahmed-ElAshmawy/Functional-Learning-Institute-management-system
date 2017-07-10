angular.module("module").controller("retrieveContact", ["$scope", "getAjax", "$http", "$location", function($scope, getAjax, $http, $location) {
    console.log("hello from Retrieve controller")

    getAjax.response("http://localhost:9408/api/Contacts/GetRetrieveContact").
    then((response) => {
        $scope.retcontact = response.data;
        console.log(response.data);
    }, (error) => { console.log(error.statusText) });


    $scope.getContact = (id) => {
        $http.put("http://localhost:9408/api/Contacts/PutContactRetrieve?id=" + id).
        then((response) => {
            $scope.retcontact = response.data;
            console.log($scope.Ret);
            $location.path("/contacts")
        }, (error) => { console.log(error.statusText) });
    }
}]);