(function () {
    angular.module('umbraco').controller("AddTranslation.CreateNewTranslation", addTranslationController);

    function addTranslationController($scope, $http, umbRequestHelper, navigationService, notificationsService ) {
        $scope.languageCode = '';
        var nodeId = $scope.this.currentNode.id;

        $scope.create = function () {
            var requestUrl = "backoffice/AddTranslation/AddTranslationApi/createLanguageVersion?nodeId=" + nodeId + "&languageCode=" + $scope.languageCode;
            console.log(requestUrl);
            umbRequestHelper.resourcePromise(
                $http.get(requestUrl),
                "New language version could not be created."
            ).then(function(){
                navigationService.hideNavigation();
                notificationsService.success("New language version of your content was created.");
            });
        };

        $scope.cancel = function () {
            navigationService.hideNavigation();
        };
    }
})();