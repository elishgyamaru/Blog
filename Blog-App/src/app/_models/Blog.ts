export interface Blog {
    id:number;
    isDraft: boolean;
    title:string;
    content:string;
    author:string;
    tags?:string[];
    category?:string;
}
