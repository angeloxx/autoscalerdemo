package com.angeloxx.bananashop;

import javax.ejb.EJB; 
import javax.ejb.Schedule;
// import javax.ejb.Timer;
import javax.ejb.Stateless;

@Stateless
public class timerBean {

	@EJB
    private counterBean counterBean;

    /**
     * Default constructor. 
     */
    public timerBean() {
        // TODO Auto-generated constructor stub
    }
	
	@Schedule(second="*/30", minute="*", hour="*", month="*", year="*", persistent=false)
    private void cleanupScheduler() {
        counterBean.cleanupClients();
    }
	
	@Schedule(second="*/10", minute="*", hour="*", month="*", year="*", persistent=false)
    private void statsScheduler() {
        counterBean.doStats();
    }
	
}