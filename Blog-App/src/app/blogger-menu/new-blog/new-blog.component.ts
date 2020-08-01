import { Component, OnInit } from '@angular/core';
import { CanDeactivateBase } from 'src/app/_guards/prevent-unsaved-changes-guard';
import { FormBuilder, Validators } from '@angular/forms';
import { BlogService } from 'src/app/_services/blog.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-new-blog',
  templateUrl: './new-blog.component.html',
  styleUrls: ['./new-blog.component.css']
})
export class NewBlogComponent implements OnInit, CanDeactivateBase {
  public toolOptions = {
    toolbar: [
      [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
      [{ 'font': [] }, { 'size': [] }],
      [ 'bold', 'italic', 'underline', 'strike' ],
      [{ 'color': [] }, { 'background': [] }],
      [{ 'script': 'super' }, { 'script': 'sub' }],
      [ 'blockquote', 'code-block' ],
      [{ 'list': 'ordered' }, { 'list': 'bullet'}, { 'indent': '-1' }, { 'indent': '+1' }],
      [ 'direction', { 'align': [] }],
      [ 'link', 'image', 'video', 'formula' ],
      ['clean']
    ]
  };
  private editorInstance: any;  
  newBlog: any = {};
  constructor(private fb: FormBuilder, 
    private router:Router,
    private blogService: BlogService) { }

  CanDeactivate(): boolean {
    return this.newBlog.dirty ? confirm("Any unsaved changes will be lost. Proceed anyway?") : true;
  }

  ngOnInit() {
    this.newBlog = this.fb.group({
      "Title": [null, Validators.required],
      "Content": [null, Validators.required]
    });
  }
  onPublish() {
    this.blogService.createNewBlog(this.newBlog.value)
    .subscribe(resp=>{
    });
  }
  onSaveAsDraft(){
    this.blogService.createNewBlog(this.newBlog.value,true)
    .subscribe(resp=>{
    });
  }
  onPreview() {
    this.blogService.previewBlog.next(this.newBlog.value);
    this.router.navigate(['preview']);
  }
  onEditorCreated(quill){
    this.editorInstance = quill;  
    var toolbar = this.editorInstance.getModule('toolbar');  
    //toolbar.addHandler('image', this.imageEditorHandler.bind(this)); 
  }
  private imageEditorHandler() {  
    if (this.editorInstance != null) {  
    const range = this.editorInstance.getSelection();  
    if (range != null) {  
        let input = document.createElement('input');  
        input.setAttribute('type', 'file');  
        input.setAttribute('accept', 'image/*');  
        input.addEventListener('change', () => {  
            if (input.files != null) {  
                let file = input.files[0];  
                if (file != null) {  
                    var reader = new FileReader();  
                    reader.readAsDataURL(file);  
                    reader.onerror = function(error) {  
                        console.log('Error: ', error);  
                    };  
                    reader.onloadend = function() {  
                        //Read complete  
                        if (reader.readyState == 2) {  
                            var base64result = reader.result;  
                            //Write your code to upload image in your custom location  
                        }  
                    };  
                }  
            }  
        });  
        input.click();  
    }  
}  
} 
}
