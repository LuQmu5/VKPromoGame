mergeInto(LibraryManager.library, {
  RequestJsPromoButtonClicked: function (id) {
    promoButtonClicked(id);
  },
  
    RequestJsCheckSubscribe: function () {
      checkSubscribe();
  },

  RequestJsOnGameSceneInited: function(){
    onGameSceneInited();
  },

  RequestJsOnSDKInited : function(){
    onSDKInited();
  },

  RequestJsOnGameCompleted : function(){
    onGameCompleted();
  },

  RequestJsOnGameStarted : function(){
    onGameStarted();
  }
});
