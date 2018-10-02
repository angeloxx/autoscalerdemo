package com.angeloxx.bananashop;

import java.util.List;

import javax.annotation.ManagedBean;
import javax.ejb.EJB;
import javax.ejb.LocalBean;
import javax.ejb.Singleton;
import javax.ejb.Startup;
import javax.management.InstanceAlreadyExistsException;
import javax.management.InstanceNotFoundException;
import javax.management.MBeanRegistrationException;
import javax.management.MBeanServer;
import javax.management.MBeanServerFactory;
import javax.management.MalformedObjectNameException;
import javax.management.NotCompliantMBeanException;
import javax.management.ObjectName;

@Singleton
@Startup
public class Prometheus implements PrometheusMBean {
	@EJB
    private CounterBean counterBean;
	
    /* (non-Javadoc)
     * @see com.angeloxx.bananashop.PrometheusMBean#getBananasCount()
     */
    public int getBananasCount() {
    	return counterBean.countBananas();
    }
    
	/* (non-Javadoc)
	 * @see com.angeloxx.bananashop.PrometheusMBean#getConcurrentClients()
	 */
	public int getConcurrentClients() {
		return counterBean.getConcurrentClients();
	}

	/* (non-Javadoc)
	 * @see com.angeloxx.bananashop.PrometheusMBean#getBananasPerMinute()
	 */
	public int getBananasPerMinute() {
		return counterBean.getBananasPerMinute();
	}

	/**
     * Default constructor. 
     * @throws MalformedObjectNameException 
     * @throws NotCompliantMBeanException 
     * @throws MBeanRegistrationException 
     * @throws InstanceAlreadyExistsException 
	 * @throws InstanceNotFoundException 
     */
	
    public Prometheus() throws MalformedObjectNameException, InstanceAlreadyExistsException, MBeanRegistrationException, NotCompliantMBeanException, InstanceNotFoundException {
    	System.out.println("Application Start Code "+this);
    	List<MBeanServer> srvrList = MBeanServerFactory.findMBeanServer(null);
    	MBeanServer server = srvrList.iterator().next();
		ObjectName name = new ObjectName("com.angeloxx.bananashop:type=CounterMBean");
		if (server.isRegistered(name)) server.unregisterMBean(name);
		server.registerMBean(this, name);
	 	
    }
}
