package com.angeloxx.bananashop;

import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.TimeUnit;

import javax.ejb.LocalBean;
import javax.ejb.Schedule;
import javax.ejb.Singleton;
import javax.ejb.Startup;
import javax.management.InstanceAlreadyExistsException;
import javax.management.MBeanRegistrationException;
import javax.management.MalformedObjectNameException;
import javax.management.NotCompliantMBeanException;

/**
 * Session Bean implementation class counter
 */
/**
 * @author aconf
 *
 */
@Startup
@Singleton
@LocalBean
public class CounterBean {

	public int bananas = 0;
	public int previousBananas = 0;
	public int bananasPerMinute = 0;
    public int bananaLimitPerformance = 70;
	public HashMap<String, Date> clients = new HashMap<String, Date>();

    /**
     * Default constructor. 
     */
    public CounterBean() {
        bananas = 0;

        if (System.getenv("MAX_BANANAS_PER_MINUTE") != null) {
            bananaLimitPerformance = Integer.parseInt(System.getenv("MAX_BANANAS_PER_MINUTE"));
        }
    }

    /**
     * Return a new banana
     * @param clientId unique identifier of the client
     */
    public void getNewBanana(String clientId) {
    	
    	if (bananasPerMinute > bananaLimitPerformance) { 
	    	try {
				TimeUnit.MILLISECONDS.sleep(250 + bananasPerMinute);
	    	} catch (InterruptedException ex) {}
    	}
    	clients.put(clientId, new Date());
        bananas++;
   }
    
    /**
     * @return global number of delivered bananas
     */
    public int countBananas() {
    	return bananas;
    }
    
    /**
     * @return number of concurrent clients in the last minute
     */
    public int getConcurrentClients() {
    	return clients.size();
    }
    
    /**
     * @return number of estimated delivered bananas per minute
     */
    public int getBananasPerMinute() {
    	return bananasPerMinute;
    }
   
    /**
     * Generate stats for the last minute based on last 10 seconds 
     */
    @Schedule(second="*/10", minute="*", hour="*", month="*", year="*", persistent=false)
    public void doStats() {
    	bananasPerMinute = (bananas - previousBananas) * 6;
    	previousBananas = bananas;
    }
    
    /**
     * Removes from the concurrent client array the older clients   
     */
    @Schedule(second="*/30", minute="*", hour="*", month="*", year="*", persistent=false)
    public void cleanupClients() {

    	Calendar lastMinute  = Calendar.getInstance();
    	lastMinute.add(Calendar.MINUTE, -1);
    	
    	for(Iterator<Map.Entry<String, Date>> item = clients.entrySet().iterator(); item.hasNext(); ) {
            Entry<String, Date> entry = item.next();
            if (entry.getValue().before(lastMinute.getTime())) {
            	System.out.println("cleanupClients removes " + entry.getKey() + " client");
            	item.remove();
            }
        }
    }
    
}
