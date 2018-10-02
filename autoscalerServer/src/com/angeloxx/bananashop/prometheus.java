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
	private int bananasCount = 0;
	
	
    public int getBananasCount() {
    	return counterBean.countBananas();
    }
    
	@Override
	public int getConcurrentClients() {
		return counterBean.getConcurrentClients();
	}

	@Override
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
