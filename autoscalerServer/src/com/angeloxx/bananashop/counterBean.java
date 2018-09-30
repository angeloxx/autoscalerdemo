package com.angeloxx.bananashop;

import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import javax.annotation.ManagedBean;
import javax.ejb.LocalBean;
import javax.ejb.Singleton;
import javax.management.Attribute;
import javax.management.AttributeNotFoundException;
import javax.management.DynamicMBean;
import javax.management.InstanceAlreadyExistsException;
import javax.management.InstanceNotFoundException;
import javax.management.InvalidAttributeValueException;
import javax.management.MBeanException;
import javax.management.MBeanRegistrationException;
import javax.management.MBeanServer;
import javax.management.MBeanServerFactory;
import javax.management.MalformedObjectNameException;
import javax.management.NotCompliantMBeanException;
import javax.management.ObjectName;
import javax.management.ReflectionException;

import lombok.Getter;


/**
 * Session Bean implementation class counter
 */
@Singleton
@LocalBean
public class counterBean {

	public int bananas = 0;
	public int previousBananas = 0;
	public int bananasPerMinute = 0;
	public HashMap<String, Date> clients = new HashMap<String, Date>();
    /**
     * Default constructor. 
     * @throws NotCompliantMBeanException 
     * @throws MBeanRegistrationException 
     * @throws InstanceAlreadyExistsException 
     * @throws MalformedObjectNameException 
     */
    public counterBean() {
        this.bananas = 0;
    }

    public void getNewBanana(String clientId) {
    	clients.put(clientId, new Date());
        bananas++;
   }
    
    public int countBananas() {
    	return bananas;
    }
    
    public int getConcurrentClients() {
    	return clients.size();
    }
    
    public int getBananasPerMinute() {
    	return bananasPerMinute;
    }
    
    /**
     * Generate stats for the last minute based on last 10 seconds 
     */
    public void doStats() {
    	bananasPerMinute = (bananas - previousBananas) * 6;
    	previousBananas = bananas;
    }
    
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
