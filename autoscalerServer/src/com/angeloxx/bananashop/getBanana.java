package com.angeloxx.bananashop;

import java.io.IOException;
import java.net.InetAddress;

import javax.annotation.ManagedBean;
import javax.ejb.EJB;
import javax.enterprise.context.SessionScoped;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * Servlet implementation class getBanana
 */
@ManagedBean
@SessionScoped
@WebServlet("/getBanana")
public class getBanana extends HttpServlet {
	private static final long serialVersionUID = 1L;
	
	@EJB
    private CounterBean counterBean;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public getBanana() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		String clientId = request.getParameter("id"); 
		
		if (clientId == null) {
			response.getWriter().append("No id, no banana!");
			return;
		}
		
		counterBean.getNewBanana(clientId);
		response.setContentType("text/html");
		response.getWriter().append("You got the banana n.").append(Integer.toString(counterBean.countBananas())).append(" from ").append(InetAddress.getLocalHost().getHostName());
	}

}
