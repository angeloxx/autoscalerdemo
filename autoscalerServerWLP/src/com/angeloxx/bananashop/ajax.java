package com.angeloxx.bananashop;

import java.io.IOException;

import javax.annotation.ManagedBean;
import javax.ejb.EJB;
import javax.enterprise.context.SessionScoped;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.json.JSONObject;


/**
 * Servlet implementation class ajax
 */
@ManagedBean
@SessionScoped
@WebServlet("/ajax")
public class ajax extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
	@EJB
    private CounterBean counterBean;
	
    /**
     * @see HttpServlet#HttpServlet()
     */
    public ajax() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		JSONObject o = new JSONObject();
		o.put("bananas", counterBean.countBananas());
		o.put("clients", counterBean.getConcurrentClients());
		
		response.setContentType("application/json");
		response.getWriter().append(o.toString());
	}

}
