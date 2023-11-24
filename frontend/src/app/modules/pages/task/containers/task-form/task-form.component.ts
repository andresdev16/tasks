import { Component, Inject, OnInit } from '@angular/core';
import { environment } from '../../../../../../environments/environment';
import { TaskAdapter, TaskOutputAdapter } from './adapters/task-adapter';
import { MasterForm } from '../../../../../core/model/master-form/master-form';
import { FormBuilder, Validators } from '@angular/forms';
import { MasterCrudService } from '../../../../../core/services/master-crud/master-crud.service';
import { OverlayContainerService } from '../../../../../core/services/overlay-container/overlay-container.service';
import { FormErrorsService } from '../../../../../core/services/form-errors/form-errors.service';
import { IControls } from '../../../../../core/interfaces/icontrols';

const paths = environment.paths;

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.scss',
  providers: [
    {
      provide: 'dependencies',
      useValue: [],
    },
    { provide: 'adapter', useValue: TaskAdapter },
    { provide: 'adapterOutput', useValue: TaskOutputAdapter },
    { provide: 'uri', useValue: paths.task },
  ],
})
export class TaskFormComponent extends MasterForm implements OnInit {
  constructor(
    protected fb: FormBuilder,
    crudService: MasterCrudService,
    overlayContainerService: OverlayContainerService,
    formErrorsService: FormErrorsService,
    @Inject('dependencies') protected override dependencies: any[],
    @Inject('adapter') protected override adapter: any,
    @Inject('adapterOutput') protected override adapterOutput: any,
    @Inject('uri') protected override uri: string,
    @Inject('dataForm') protected override dataForm: any
  ) {
    super(
      fb,
      crudService,
      overlayContainerService,
      formErrorsService,
      dependencies,
      adapter,
      adapterOutput,
      uri,
      dataForm
    );
  }

  override ngOnInit(): void {
    this.addControls(this._loadForm());
    super.ngOnInit();
  }

  private _loadForm(): IControls[] {
    return [
      {
        name: 'name',
        control: this.fb.control('', Validators.required),
      },
      {
        name: 'category',
        control: this.fb.control('', Validators.required),
      },
      {
        name: 'limitDate',
        control: this.fb.control('', Validators.required),
      },
    ];
  }
}
