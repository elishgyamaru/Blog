import { FormControl, FormGroup } from '@angular/forms';

export function MatchValidator(controlName:string,matchingControlName:string){
    return (formGroup:FormGroup)=>{
        let control=formGroup.get(controlName);
        let matchingControl=formGroup.get(matchingControlName);
        if(control.value===matchingControl.value)
        {
            return;
        }
        matchingControl.setErrors({PasswordMismatch:true})
        return {passwordMismatch:true}
    }
}
