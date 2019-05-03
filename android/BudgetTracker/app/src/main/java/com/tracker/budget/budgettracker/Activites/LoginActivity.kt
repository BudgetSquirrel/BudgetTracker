package com.tracker.budget.budgettracker.Activites

import android.content.Context
import android.content.Intent
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.support.design.widget.NavigationView
import android.support.v4.app.Fragment
import android.support.v7.app.ActionBarDrawerToggle
import android.support.v7.widget.Toolbar
import com.tracker.budget.budgettracker.LoginFragment
import com.tracker.budget.budgettracker.R
import com.tracker.budget.budgettracker.SettingsFragment
import kotlinx.android.synthetic.main.activity_login.*

fun Context.openLoginActivity() : Intent {
    return Intent()
}

class LoginActivity : AppCompatActivity() {

    var existingFragment: Fragment? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        var toolbar: Toolbar = findViewById(R.id.home_toolbar)
        setSupportActionBar(toolbar)
        supportActionBar?.setDisplayShowTitleEnabled(false)

        toolbar.setNavigationIcon(R.drawable.ic_hamburger)

        val drawerToggle = ActionBarDrawerToggle(this,
            login_drawer_layout, toolbar, R.string.drawer_open, R.string.drawer_close)
        login_drawer_layout.addDrawerListener(drawerToggle)
        drawerToggle.syncState()
        setupDrawerContent(login_drawer)
        initLoginFragment()
    }

    private fun initLoginFragment() {
        if (login_container != null) {
            val fragmentTransaction = supportFragmentManager.beginTransaction()
            fragmentTransaction.add(R.id.login_container, LoginFragment())
            fragmentTransaction.commit()
            existingFragment = LoginFragment()
        }
        title = getString(R.string.login)
    }


    private fun setupDrawerContent(navigationView: NavigationView) {
        navigationView.setNavigationItemSelectedListener {
            if(it.itemId == R.id.login_menu) {
                supportFragmentManager.beginTransaction().replace(R.id.login_container, LoginFragment()).addToBackStack(null).commit()
            } else if(it.itemId == R.id.settings_menu) {
                supportFragmentManager.beginTransaction().replace(R.id.login_container, SettingsFragment()).addToBackStack(null).commit()
            }

            title = it.title
            login_drawer_layout.closeDrawers()
            true
        }
    }

}
