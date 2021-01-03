using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DasBlog.Managers.Interfaces;
using DasBlog.Services;
using DasBlog.Web.Models.BlogViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DasBlog.Web.TagHelpers.Site
{
	public class SiteLastestBlogPostsTagHelper : TagHelper
	{

		private readonly IBlogManager blogManager;
		private readonly IUrlHelper urlHelper;
		private readonly IDasBlogSettings dasBlogSettings;
		private readonly IMapper mapper;

		public SiteLastestBlogPostsTagHelper(IBlogManager blogManager, IDasBlogSettings dasBlogSettings, IMapper mapper, IHttpContextAccessor accessor)
		{
			this.dasBlogSettings = dasBlogSettings;
			this.blogManager = blogManager;
			this.mapper = mapper;
			urlHelper = accessor.HttpContext?.Items[typeof(IUrlHelper)] as IUrlHelper;
			if (urlHelper == null)
			{
				throw new Exception("No UrlHelper found in http context");
			}
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = string.Empty;
			output.Content.Clear();

			var latestEntries = blogManager.GetLastestPosts("en-US,en;q=0.5", 3)
				.Select(entry => mapper.Map<PostViewModel>(entry))
				.Select(editentry => editentry).ToList();

			string outputHTML = "";

			foreach(var entry in latestEntries)
			{
				outputHTML += $@"
					<div class='page-footer__recent-post'>
						<div class='page-footer__recent-post-info'>
							<div class='page-footer__recent-post-content'>
								<a href='" + entry.PermaLink + "'>" + entry.Title + "</a>" +
								"</div>" +
								"<div class='page-footer__recent-post-date'>" +
								"<span>" + entry.CreatedDateTime.ToString("MMMM dd yyyy") + "</span>" +
								"</div>" +
								"</div>" +
								"</div>";
			}	

			output.Content.AppendHtml($@"
				<div class='col-sm-4 col-md-4'>
					<h4> Recent Posts </h4>"
					+ outputHTML + " </div>");
		}
		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			return Task.Run(() => Process(context, output));
		}
	}
}
