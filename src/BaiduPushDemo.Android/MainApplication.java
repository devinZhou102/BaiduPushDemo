package com.baidu.push.app;

import android.app.Application;
import android.util.Log;

import com.baidu.android.pushservice.PushConstants;
import com.baidu.android.pushservice.PushManager;

public class MainApplication extends Application {

    /**
     * TAG to Log
     */
    public static final String TAG = MainApplication.class
            .getSimpleName();

    @Override
    public void onCreate() {
        super.onCreate();

        Log.d(TAG, "onCreate");
		
        //PushManager.startWork(getApplicationContext(), PushConstants.LOGIN_TYPE_API_KEY,
          //      Utils.getMetaValue(this, "api_key"));
				
        PushManager.startWork(getApplicationContext(), PushConstants.LOGIN_TYPE_API_KEY,"hhN9CFi3gZPpm9LD5szBT8hn");
    }
}
