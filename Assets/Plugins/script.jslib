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

<<<<<<< HEAD
  RequestJsGetPromo: function(){
    getPromo();
  },
=======
  RequestJsGetPromo : function(){
    getPromo();
  }
>>>>>>> a0fba0e449c9f4e83395aa9cfac568667c3f5883
});
