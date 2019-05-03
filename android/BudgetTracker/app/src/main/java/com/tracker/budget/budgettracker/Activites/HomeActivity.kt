package com.tracker.budget.budgettracker.Activites

import android.content.Context
import android.content.Intent
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.support.design.widget.NavigationView
import android.support.v7.app.ActionBarDrawerToggle
import android.support.v7.widget.Toolbar
import com.tracker.budget.budgettracker.HomeFragment
import com.tracker.budget.budgettracker.LoginFragment
import com.tracker.budget.budgettracker.R
import com.tracker.budget.budgettracker.SettingsFragment
import kotlinx.android.synthetic.main.activity_home.*
import kotlinx.android.synthetic.main.activity_login.*

fun Context.openHomeActivity() : Intent {
    return Intent()
}

class HomeActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)

        var toolbar: Toolbar = findViewById(R.id.home_toolbar)
        setSupportActionBar(toolbar)
        supportActionBar?.setDisplayShowTitleEnabled(false)

        toolbar.setNavigationIcon(R.drawable.ic_hamburger)

        val drawerToggle = ActionBarDrawerToggle(this,
            home_drawer_layout, toolbar, R.string.drawer_open, R.string.drawer_close)
        home_drawer_layout.addDrawerListener(drawerToggle)
        drawerToggle.syncState()
        setupDrawerContent(login_drawer)
    }

    private fun setupDrawerContent(navigationView: NavigationView) {
        navigationView.setNavigationItemSelectedListener {
            if(it.itemId == R.id.home_menu) {
                supportFragmentManager.beginTransaction().replace(R.id.home_container, HomeFragment()).addToBackStack(null).commit()
            } else if(it.itemId == R.id.settings_menu) {
                supportFragmentManager.beginTransaction().replace(R.id.home_container, SettingsFragment()).addToBackStack(null).commit()
            } else if(it.itemId == R.id.logout_menu) {
                startActivity(this.openLoginActivity())
            }

            title = it.title
            home_drawer_layout.closeDrawers()
            true
        }
    }

}
