package com.tracker.budget.budgettracker.Activites

import android.content.Context
import android.content.Intent
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import com.tracker.budget.budgettracker.R

fun Context.openHomeActivity() : Intent {
    return Intent()
}

class HomeActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)
    }
}
