package com.tracker.budget.budgettracker.Activites

import android.content.Context
import android.content.Intent
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.support.design.widget.NavigationView
import android.support.v7.app.ActionBarDrawerToggle
import android.support.v7.widget.Toolbar
import com.tracker.budget.budgettracker.R
import kotlinx.android.synthetic.main.activity_login.*
import android.support.annotation.NonNull
import android.R
import android.view.MenuItem


fun Context.openLoginActivity() : Intent {
    return Intent()
}

class LoginActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        var toolbar: Toolbar
        toolbar = findViewById(R.id.login_toolbar)
        setSupportActionBar(toolbar)
        supportActionBar?.setDisplayShowTitleEnabled(false)

        toolbar.setNavigationIcon(R.drawable.ic_hamburger)

        val drawerToggle = ActionBarDrawerToggle(this,
            login_drawer_layout, toolbar, R.string.drawer_open, R.string.drawer_close)
        login_drawer_layout.addDrawerListener(drawerToggle)
        drawerToggle.syncState()

    }

    private fun setupDrawerContent(navigationView: NavigationView) {
        navigationView.setNavigationItemSelectedListener {
            //presenter.drawerItemSelected(menuItem.getItemId())
            true
        }
    }
}
