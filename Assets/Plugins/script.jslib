mergeInto(LibraryManager.library, {
  RequestJsClaimReward: function (id) {
    claimReward(id);
  },
  
  RequestJsCheckSubscribe: function () {
    checkSubscribe();
  },

  RequestJsOnGameSceneInited: function(){
    onGameSceneInited();
  },

  RequestJsOnGameCompleted : function(){
    onGameCompleted();
  },

  RequestJsOnGameStarted : function(){
    onGameStarted();
  },

  RequestJsGetPromoCode: function(str){
    getPromoCode(str);
  }
});
