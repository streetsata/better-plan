using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Entities.Models
{
    public class Post
    {
        /// <summary>
        /// ID поста в БД
        /// </summary>
        [Key]
        public Int32 PostId { get; set; }

        /// <summary>
        /// Id User
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id поста в facebook
        /// </summary>
        public string FacebookPostId { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Время сохранения поста
        /// </summary>
        public DateTime? SaveCreateDateTime { get; set; }

        /// <summary>
        /// Время изменения поста
        /// </summary>
        public DateTime? SaveUpdateDateTime { get; set; }

        /// <summary>
        /// Время Удаления поста
        /// </summary>
        public DateTime? SaveDeleteDateTime { get; set; }

        /// <summary>
        /// Время создания поста
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// Время обновления(изменения) поста
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// Время Удаления поста
        /// </summary>
        public DateTime? DeleteDateTime { get; set; }

        /// <summary>
        /// Удален ли пост
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// List картинок
        /// </summary>
        public String ImagesListJSON { get; set; }

        /// <summary>
        /// List картинок
        /// </summary>
        //public List<String> ArrayURLImages { get; set; }

        /// <summary>
        /// Время отложенного постинга
        /// </summary>
        public DateTime? WhenCreateDateTime { get; set; }

        /// <summary>
        /// Публиковать пост
        /// </summary>
        public Boolean isPosting { get; set; }

        /// <summary>
        /// Публикуется отложенный пост
        /// </summary>
        public Boolean isWaiting { get; set; }

        /// <summary>
        /// Статус публикации
        /// </summary>
        public Status status { get; set; }
        public Post(string userId, PostViewModel postViewModel)
        {
            this.UserId = userId;

            this.Text = postViewModel.post_text;

            this.IsDelete = false;
            this.isPosting = postViewModel.isPosting;
            this.WhenCreateDateTime = postViewModel.WhenCreateDateTime;

            if (postViewModel.ImagesListIFormFile != null)
            {
                if (postViewModel.ImagesURLList == null)
                {
                    this.ImagesListJSON = JsonConvert.SerializeObject(this.ImgJSON(postViewModel.ImagesListIFormFile).Result, Formatting.None);
                }
                else
                {
                    var res = this.ImgJSON(postViewModel.ImagesListIFormFile).Result;
                    var list = postViewModel.ImagesURLList;
                    for (int i = 0; i < res.Count; i++)
                    {
                        list.Add(res[i]);
                    }
                    this.ImagesListJSON = JsonConvert.SerializeObject(list, Formatting.None);
                }

            }

        }

        public Post(string userId, PostViewModelFiles postViewModel)
        {
            this.UserId = userId;

            this.Text = postViewModel.post_text;

            this.IsDelete = false;
            this.isPosting = postViewModel.isPosting;
            this.WhenCreateDateTime = postViewModel.WhenCreateDateTime;

            if (postViewModel.ImagesListIFormFile != null)
            {
                if (postViewModel.ImagesURLList == null)
                {
                    this.ImagesListJSON = JsonConvert.SerializeObject(this.ImgJSON(postViewModel.ImagesListIFormFile).Result, Formatting.None);
                }
                else
                {
                    var res = this.ImgJSON(postViewModel.ImagesListIFormFile).Result;
                    var list = postViewModel.ImagesURLList;
                    for (int i = 0; i < res.Count; i++)
                    {
                        list.Add(res[i]);
                    }
                    this.ImagesListJSON = JsonConvert.SerializeObject(list, Formatting.None);
                }

            }

        }

        public Post()
        {

        }

        public Post(string userId, SaveViewModelFiles saveViewModel)
        {
            this.UserId = userId;

            this.Text = saveViewModel.post_text;

            if (saveViewModel.ImagesListIFormFile != null)
            {
                if (saveViewModel.ImagesURLList == null)
                {
                    this.ImagesListJSON = JsonConvert.SerializeObject(this.ImgJSON(saveViewModel.ImagesListIFormFile).Result, Formatting.None);
                }
                else
                {
                    var res = this.ImgJSON(saveViewModel.ImagesListIFormFile).Result;
                    var list = saveViewModel.ImagesURLList;
                    for (int i = 0; i < res.Count; i++)
                    {
                        list.Add(res[i]);
                    }
                    this.ImagesListJSON = JsonConvert.SerializeObject(list, Formatting.None);
                }

            }
        }

        public Post(string userId, SaveViewModel saveViewModel)
        {
            this.UserId = userId;
            this.Text = saveViewModel.post_text;
        }


        public void Copy(PostViewModel model)
        {
            this.Text = model.post_text;

            this.IsDelete = false;
            this.isPosting = model.isPosting;
            this.WhenCreateDateTime = model.WhenCreateDateTime;

            if (model.ImagesListIFormFile != null)
            {
                if (model.ImagesURLList == null)
                {
                    this.ImagesListJSON = JsonConvert.SerializeObject(this.ImgJSON(model.ImagesListIFormFile).Result, Formatting.None);
                }
                else
                {
                    var res = this.ImgJSON(model.ImagesListIFormFile).Result;
                    var list = model.ImagesURLList;
                    for (int i = 0; i < res.Count; i++)
                    {
                        list.Add(res[i]);
                    }
                    this.ImagesListJSON = JsonConvert.SerializeObject(list, Formatting.None);
                }

            }
        }

        public void Copy(PostViewModelFiles model)
        {
            this.Text = model.post_text;

            this.IsDelete = false;
            this.isPosting = model.isPosting;
            this.WhenCreateDateTime = model.WhenCreateDateTime;

            if (model.ImagesListIFormFile != null)
            {
                if (model.ImagesURLList == null)
                {
                    this.ImagesListJSON = JsonConvert.SerializeObject(this.ImgJSON(model.ImagesListIFormFile).Result, Formatting.None);
                }
                else
                {
                    var res = this.ImgJSON(model.ImagesListIFormFile).Result;
                    var list = model.ImagesURLList;
                    for (int i = 0; i < res.Count; i++)
                    {
                        list.Add(res[i]);
                    }
                    this.ImagesListJSON = JsonConvert.SerializeObject(list, Formatting.None);
                }

            }
        }

        public void Copy(SaveViewModel model)
        {
            this.Text = model.post_text;
            this.IsDelete = false;
        }

        public void Copy(SaveViewModelFiles model)
        {
            this.Text = model.post_text;

            this.IsDelete = false;

            if (model.ImagesListIFormFile != null)
            {
                if (model.ImagesURLList == null)
                {
                    this.ImagesListJSON = JsonConvert.SerializeObject(this.ImgJSON(model.ImagesListIFormFile).Result, Formatting.None);
                }
                else
                {
                    var res = this.ImgJSON(model.ImagesListIFormFile).Result;
                    var list = model.ImagesURLList;
                    for (int i = 0; i < res.Count; i++)
                    {
                        list.Add(res[i]);
                    }
                    this.ImagesListJSON = JsonConvert.SerializeObject(list, Formatting.None);
                }

            }
        }

        private async Task<List<String>> ImgJSON(List<IFormFile> formFiles)
        {
            List<String> linksList = new List<string>();

            var uploads = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            foreach (var imageFile in formFiles)
            {
                var filePath = Path.Combine(uploads, Guid.NewGuid() + imageFile.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    linksList.Add(filePath);
                    await imageFile.CopyToAsync(fileStream);
                }
            }

            return linksList;// JsonConvert.SerializeObject(linksList,Formatting.None); //JsonConvert.SerializeObject(linksList, Formatting.None);
        }
    }
}
