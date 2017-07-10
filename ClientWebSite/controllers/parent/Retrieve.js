angular.module("module").controller("Retrieve", ["$scope", "getAjax", "$http", "$location", function ($scope, getAjax, $http, $location) {

console.log("hello from Retrieve controller")
    getAjax.response("http://localhost:9408/api/Parents/GetRetrieveParent").
    then((response) => {
        $scope.Ret = response.data;
        console.log($scope.Ret);
    }, (error) => { console.log(error.statusText) });
   

   $scope.GetParent=(id)=>
   {
       $http.put("http://localhost:9408/api/Parents/PutParentRetrieve?id=" + id).
    then((response) => {
        $scope.Ret = response.data;
        $location.path("/parent")
        console.log($scope.Ret);
    }, (error) => { console.log(error.statusText) });
   }

}]);