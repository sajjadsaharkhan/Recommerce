using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Recommerce.Services.Recommend;
using Recommerce.Services.Recommend.Dto;
using Recommerce.ViewModels.Recommend;

namespace Recommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class RecommendController : ControllerBase
{
    private readonly IRecommenderService _recommenderService;

    public RecommendController(IRecommenderService recommenderService)
    {
        _recommenderService = recommenderService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]RecommendationInVm recommendationInVm, CancellationToken cancellationToken)
    {
        var validator = new RecommendationInVmValidator();
        var validationResult = await validator.ValidateAsync(recommendationInVm, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var recommendationInDto = recommendationInVm.Adapt<RecommendationInDto>();
        var recommendationResult = await _recommenderService.GetRecommendationAsync(recommendationInDto, cancellationToken);
        if (recommendationResult.IsFailed)
            return NotFound(recommendationResult.Exception.Message);
        
        if (recommendationResult.Data is { Count: 0 })
            return NoContent();
        
        return Ok(recommendationResult.Data);
    }
}