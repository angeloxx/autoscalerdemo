package com.angeloxx.bananashop;

/**
 * @author aconf
 *
 */
public interface PrometheusMBean {

	/**
	 * Get global delivered bananas counter
	 * @return delivered bananas
	 */
	public int getBananasCount();
	
	/**
	 * Get concurrent client (in the last minute)
	 * @return number of clients
	 */
	public int getConcurrentClients();
	
	/**
	 * Get estimated bananas per minute, based on last 10 secs stats
	 * @return number of bananas per minute
	 */
	public int getBananasPerMinute();
}
