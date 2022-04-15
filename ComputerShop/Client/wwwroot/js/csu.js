function goToElementById(id)
{
    document.getElementById(id).scrollIntoView({ behavior: "smooth", block: "start", inline: "start" });
}