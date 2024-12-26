mergeInto(LibraryManager.library, {
  RequestJsClaimReward: function (id) {
    claimReward(id);
  },
  
  RequestJsCheckSubscribe: function () {
    checkSubscribe();
  },

  // не успевает загрузить
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
