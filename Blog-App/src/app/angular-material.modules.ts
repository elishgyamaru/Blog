import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatTabsModule} from '@angular/material/tabs';
import {MatMenuModule} from '@angular/material/menu';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
    declarations:[],
    imports:[
        CommonModule
    ],
    exports:[
        MatSidenavModule,
        MatListModule,
        MatToolbarModule,
        MatButtonModule,
        MatTabsModule,
        MatMenuModule,
    ]
})
export class AngularMaterialModules{}