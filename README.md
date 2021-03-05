# Introduction 
The XConnect Migration code sample illustrates how data can be migrated from one version of XConnect to another and shows the flow of data migration between **XConnect 9.3** to **XConnect 10.1.0**.

# Getting Started
The code sample is based on the XConnect Client API, which allows trusted clients to extract data from the source instance of XConnect and create data in the target instance of XConnect.

The contact data extraction is a function of the XConnect Client API that allows all or a subset of contacts and interactions to be exported.
Contacts along with all related facets, identifiers, and interactions are extracted from the source XConnect and submitted to the target XConnect in batches.

## Pre-requisites and Post Actions
Performing the list of pre-requisite actions and post-steps is not mandatory.

Please, take into account that the source Sitecore items have to be fully migrated to the target Sitecore as part of the upgrade process.
For instance, if you want to keep the same Contact lists for List Manager you need to migrate list items together with the `ListSubscriptions` contact facet to keep the same behavior.

The code sample is based on the contact data extraction API. Whenever the process of extracting data is started, the API fixes the latest added data and continues reading all the collected data in revese order.
As a result, it is not possible to retrieve the data that is collected after the extraction is started.

You can, however, re-run the tool to retrieve the most current data.

### The source XConnect instance
**Pre-requisites**

The following list contains suggested pre-requisite checks for the source XConnect instance:
1. Verify that XConnect is not collecting any new data.
2. Verify that the Marketing Automation pool is empty and there is no ongoing task processing.
3. Verify that the Processing Pool is empty and there are no active or different items processing.
4. Verify that the Tracker Submit Queue is empty and there is no ongoing data submission.

**Post-actions**

There are no post-actions for the source XConnect.

The list of actions is not mandatory, but be aware that the latest collected data might missing after the migration process is complete.

### The target XConnect instance

There are two options that can be used for the target XConnect instance.

#### Option #1 - Do not disable anything on the target instance

**Pre-requisites**

None.

This means that the indexer as well as the Marketing Automation and Processing Engine will be under the load during the migration and this might lead to performance lags.

**Post-actions**

None.

#### Option #2 - Disable some of the heavy processing services
1. Disable *Change Tracking* on the xDB databases.
2. Disable *Indexer* service.
3. Disabled *Marketing Automation* plugin.
4. Disabled *Processing* plugin.

For Option #2 there is a list of post-actions that are mandatory after migration.

**Post-actions**
1. Enable Change Tracking on the xDB databases.
2. Enable Indexer service.
3. Enable Marketing Automation plugin.
2. Rebuild the 'xdb' index.
3. Rebuild the Reporting database.

The code sample shows how to work with multiple versions of the XConnect Clients with the help of the Application Domains.

# Build and Run
1. Restore packages

The code sample uses the XConnect packages officially released from the [MyGet](#https://sitecore.myget.org/F/sc-packages/api/v3/index.json) feed.
```xml
    <PackageReference Include="Sitecore.ContentTesting.Model.xConnect" Version="9.3.0" />
    <PackageReference Include="Sitecore.EmailCampaign.Model" Version="9.3.0" />
    <PackageReference Include="Sitecore.XConnect.Client" Version="9.3.0" />
    <PackageReference Include="Sitecore.XConnect.Collection.Model" Version="9.3.0" />
```
```xml
    <PackageReference Include="Sitecore.ContentTesting.Model.xConnect" Version="10.1.0" />
    <PackageReference Include="Sitecore.EmailCampaign.Model" Version="10.1.0" />
    <PackageReference Include="Sitecore.XConnect.Client" Version="10.1.0" />
    <PackageReference Include="Sitecore.XConnect.Collection.Model" Version="10.1.0" />
```

The Serilog package for logging can be restored from the [Nuget.org](#https://www.nuget.org/api/v2) feed.

2. Configure endpoints and certificates for the source and target XConnect instances in the configuration file.
```xml
    <!-- Target connection strings -->
    <add name="xconnect.collection.101" connectionString="https://sitecore10.1.0.xconnect" />
    <add name="xconnect.collection.certificate.101" connectionString="StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=" />
    <add name="xconnect.search.101" connectionString="https://sitecore10.1.0.xconnect" />
    <add name="xconnect.search.certificate.101" connectionString="StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=" />
    <add name="xconnect.configuration.101" connectionString="https://sitecore10.1.0.xconnect" />
    <add name="xconnect.configuration.certificate.101" connectionString="StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=" />
    <!-- Source connection strings -->
    <add name="xconnect.collection.93" connectionString="https://sitecore9.3.xconnect" />
    <add name="xconnect.collection.certificate.93" connectionString="StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=" />
    <add name="xconnect.search.93" connectionString="https://sitecore9.3.xconnect" />
    <add name="xconnect.search.certificate.93" connectionString="StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=" />
    <add name="xconnect.configuration.93" connectionString="https://sitecore9.3.xconnect" />
    <add name="xconnect.configuration.certificate.93" connectionString="StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=" />
```

3. Build the project and try to execute **Sitecore.XConnect.DataMigration.Tool** console application.

# Data migration
## XConnect core model migration
### Contacts
The contact data extraction is a function of the XConnect Client API that allows all or a subset of contacts and interactions to be exported.
The contacts and interactions are extracted from the source XConnect and submitted to the target XConnect in batches.

The data migration uses the XConnect API to submit the migrated contact to the target XConnect, so the source and target contacts will have different contact ids.

It is possible to configure the batch size for both the data extraction and contact submission.

```xml
<add key = "Source.ContactExtraction.BatchSize" value="50"/>
<add key = "Target.Submit.BatchSize" value="10"/>
```
The code sample uses a producer-consumer dataflow pattern so that only one thread extracts data and one or many threads submit data to the target XConnect. The number of threads that submit data can be set in the configuration file.

```xml
<add key = "Target.Submit.NumberOfThreads" value ="2" />
```

By default, not all the extracted contacts are migrated to the target XConnect. The code sample uses the concept of contact filters to skip contacts that do not need to be processed.

#### Contact Filters
It is possible to filter contacts that do not need to be migrated with the help of contact filters.
The code sample illustrates how to skip contact migration using specified conditions.
The default list of registered conditions:
* Skip anonymous contacts
* Skip contacts tagged with Right-to-be-forgotten
* Skip source contacts that were merged into target contacts

It is possible to add any conditions for skipping contact migration.
The condition must implement `IContactFilter` interface and be registered
in the `ContactFilterManager`.

```csharp
    public interface IContactFilter
    {
        bool Apply(Contact contact);
    }
```
```csharp
    public ContactFilterManager()
    {
        _contactFilters = new List<IContactFilter>
        {
            new AnonymousContactFilter(),
            new ForgottenContactFilter(),
            new MergedContactFilter()
        };
    }
```
### Contact Identifiers
The contact is migrated with all anonymous and known identifiers except the system ones.

The anonymous system identifiers:
* Alias identifier
* Merge identifier

#### Alias identifier
By default, all contacts have at least one anonymous alias identifier.
The alias identifier is a system identifier generated by the XConnect service and it cannot be explicitly added to the contact.
The source alias identifier is migrated to the target XConnect with the new identifier source value - `AliasOld`.
This helps keep a contact link between the source and target XConnect instances and ensures the same contact is not migrated twice.

Take into account, that if your solution has any direct references to the contact via alias identifier the identifier source needs to be updated or a new alias must be used.

#### Contact merge identifier
By default, all merged contacts are not migrated. This is configured in the `ContactFilterManager`.
The `Merge` identifier is explictily skipped during migration with the help of the identifier filter.

#### Identifier filters
It is possible to filter contact identifiers that do not need to be migrated with the help of the contact identifier filters.
The code sample shows how to skip contact identifier migration using specified conditions.

The default list of registered conditions:
* Skip `Merge` identifiers

It is possible to add any conditions to skip the contact identifier migration.
The condition must implement `IContactIdentifierFilter` interface and be registered
in the `ContactIdentifierFilterManager`.

```csharp
    public interface IContactIdentifierFilter
    {
        bool Apply(ContactIdentifier contactIdentifier);
    }
```
```csharp
    public ContactIdentifierFilterManager()
    {
       _contactIdentifiersFilters = new List<IContactIdentifierFilter>
       {
           new MergeIdentifierFilter()
        };
    }
```


### Contacts and Interaction Facets
The list of contact and interaction facets that must be migrated is configurable.
The facet keys are delimitted by a comma `,` in the configuration file.

By default, the code sample is configured to migrate all core facets.
```xml
<add key ="Source.ContactFacetKeys" value="AutomationPlanEnrollmentCache,AutomationPlanExit,EmailAddressHistory,ConsentInformation,MergeInfo,Emails,Personal,Addresses,Avatar,Classification,PhoneNumbers,ListSubscriptions,TestCombinations"/>
<add key ="Source.InteractionFacetKeys" value="WebVisit,IpInfo,ProfileScores,LocaleInfo,UserAgentInfo"/>
```

The migration facet process is quite simple if there are no changes between the source and target model versions. The code sample uses built-in serialization to transfer data between instances.

#### Customize core facets migration
The core model in the source and target XConnect might differ, so it is possible to customize the way facets are migrated.

For instance, there is a difference between **XConnect Model 9.3** and **XConnect Model 10.1.0** for the following facets:
* ConsentInformation
* EmailAddressHistory

The code sample illustrates how to customize facet migration with the help of facet mappers.
Each facet that requires custom migration must implement the facet model and facet mappers that need to be registered in the mapper registry.

##### Facet Model
The facet model is the represenation of the facet data that will be transferred between XConnect instances.
The facet model has to be inherited from the `FacetModel` base class.

All facet models must be marked as `[Serializable]`.

```csharp
    [Serializable]
    public class ConsentInformationFacetModel : FacetModel
    {
        public bool ConsentRevoked { get; set; }

        public bool DoNotMarket { get; set; }

        public bool ExecutedRightToBeForgotten { get; set; }
    }
```

##### Facet Mappers
There are two types of facet mappers included in the code sample:
* Facet to model mapper
* Model to facet mapper

The Facet-to-model mapper translates the source XConnect facet to the facet model.
The mapper must be inherited from the `BaseFacetMapper<TFacet, TFacetModel>` and registered in the `FacetMapperRegistry`:

```csharp
    public class ConsentInformationFacetMapper : BaseFacetMapper<ConsentInformation, ConsentInformationFacetModel>
    {
        protected override ConsentInformationFacetModel CreateModel(ConsentInformation sourceFacet)
        {
            return new ConsentInformationFacetModel
            {
                ConsentRevoked = sourceFacet.ConsentRevoked,
                DoNotMarket = sourceFacet.DoNotMarket,
                ExecutedRightToBeForgotten = sourceFacet.ExecutedRightToBeForgotten
            };
        }
    }
```

```csharp
    public FacetMapperRegistry()
    {
         _serviceProvider = new ServiceCollection()
            .AddSingleton<IFacetMapper<ConsentInformation>, ConsentInformationFacetMapper>()
            .AddSingleton<IFacetMapper<EmailAddressHistory>, EmailAddressHistoryFacetMapper>()
            .BuildServiceProvider();
    }
```

The Model-to-facet mapper translates the facet model to the target XConnect facet.
The mapper must be inherited from the `BaseFacetModel<TFacetModel, TFacet>` and registered in the `FacetModelMapperRegistry`:

```csharp
    public class ConsentInformationFacetModelMapper : BaseFacetModelMapper<ConsentInformationFacetModel, ConsentInformation>
    {
        protected override ConsentInformation CreateFacet(ConsentInformationFacetModel sourceFacet)
        {
            return new ConsentInformation
            {
                ConsentRevoked = sourceFacet.ConsentRevoked,
                DoNotMarket = sourceFacet.DoNotMarket,
                ExecutedRightToBeForgotten = sourceFacet.ExecutedRightToBeForgotten
            };
        }
    }
```

```csharp
    public FacetModelMapperRegistry()
    {
         _serviceProvider = new ServiceCollection()
            .AddSingleton<IFacetModelMapper<ConsentInformationFacetModel>, ConsentInformationFacetModelMapper>()
            .AddSingleton<IFacetModelMapper<EmailAddressHistoryFacetModel>, EmailAddressHistoryFacetModelMapper>()
            .BuildServiceProvider();
    }
```

#### Calculated Contact Facets
Calculated facets are not migrated by the tool explicitly. They are updated by the XConnect service layer each time a new interaction is submitted.
Interactions are migrated in an unpredictable order and, as a result, the target XConnect calculated facets might differ from the source XConnect.

### Interaction Events
All interaction events are migrated by default.
The migration of interaction events is based on the event model mappers.
Each XConnect interaction event that needs to be migrated implements the event model and event mappers that must be registered in the mapper registry.

#### Interaction Event Model
The event model is the represenation of the event data that will be transfered between XConnect instances.
The event model has to be inherited from the `EventModel` base class.

All the event models must be marked as `[Serializable]`.

```csharp
    [Serializable]
    public class SearchEventModel : EventModel
    {
        public string Keywords { get; set; }
    }
```

#### Interaction Event Mappers
There are two types of event mapper presented in the code sample:
* XConnect event to event model mapper
* Event model to XConnect event mapper

The XConnect event-to-model mapper translates the source XConnect event to the event model.
The mapper has to be inherited from `BaseEventMapper<TEvent, TEventModel>` and registered in the `EventModelMapperRegistry`:

```csharp
    public class SearchEventMapper : BaseEventMapper<SearchEvent, SearchEventModel>
    {
        protected override SearchEventModel CreateModel(SearchEvent sourceEvent)
        {
            return new SearchEventModel
            {
                Keywords = sourceEvent.Keywords
            };
        }
    }
```

```csharp
    public EventMapperRegistry()
    {
        _serviceProvider = new ServiceCollection()
            .AddSingleton<IEventMapper<Event>, EventMapper>()
            .AddSingleton<IEventMapper<Goal>, GoalMapper>()
            .AddSingleton<IEventMapper<Outcome>, OutcomeMapper>()
            .AddSingleton<IEventMapper<CampaignEvent>, CampaignEventMapper>()
            .AddSingleton<IEventMapper<PageViewEvent>, PageViewEventMapper>()
            .AddSingleton<IEventMapper<DownloadEvent>, DownloadEventMapper>()
            .AddSingleton<IEventMapper<SearchEvent>, SearchEventMapper>()
            .AddSingleton<IEventMapper<MVTestTriggered>, MVTestTriggeredMapper>()
            .AddSingleton<IEventMapper<PersonalizationEvent>, PersonalizationEventMapper>()
            .AddSingleton<IEventMapper<BounceEvent>, BounceEventMapper>()
            .AddSingleton<IEventMapper<DispatchFailedEvent>, DispatchFailedEventMapper>()
            .AddSingleton<IEventMapper<EmailClickedEvent>, EmailClickedEventMapper>()
            .AddSingleton<IEventMapper<EmailOpenedEvent>, EmailOpenedEventMapper>()
            .AddSingleton<IEventMapper<EmailSentEvent>, EmailSentEventMapper>()
            .AddSingleton<IEventMapper<SpamComplaintEvent>, SpamComplaintEventMapper>()
            .AddSingleton<IEventMapper<UnsubscribedFromEmailEvent>, UnsubscribedFromEmailEventMapper>()
            .AddSingleton<IEventMapper<EmailEvent>, EmailEventMapper>()
            .BuildServiceProvider();
    }
```

The model-to-event mapper translates the event model to the target XConnect event.
It must be inherited from the `BaseEventModelMapper<TEventModel, TEvent>` and registered in the `EventModelMapperRegistry`:

```csharp
    public class SearchEventModelMapper : BaseEventModelMapper<SearchEventModel, SearchEvent>
    {
        protected override SearchEvent CreateEvent(SearchEventModel sourceEvent)
        {
            return new SearchEvent(sourceEvent.Timestamp)
            {
                Keywords = sourceEvent.Keywords
            };
        }
    }
```

```csharp
   public EventModelMapperRegistry()
    {
        _serviceProvider = new ServiceCollection()
            .AddSingleton<IEventModelMapper<EventModel>, EventModelMapper>()
            .AddSingleton<IEventModelMapper<GoalModel>, GoalModelMapper>()
            .AddSingleton<IEventModelMapper<OutcomeModel>, OutcomeModelMapper>()
            .AddSingleton<IEventModelMapper<CampaignEventModel>, CampaignEventModelMapper>()
            .AddSingleton<IEventModelMapper<PageViewEventModel>, PageViewEventModelMapper>()
            .AddSingleton<IEventModelMapper<DownloadEventModel>, DownloadEventModelMapper>()
            .AddSingleton<IEventModelMapper<SearchEventModel>, SearchEventModelMapper>()
            .AddSingleton<IEventModelMapper<MVTestTriggeredModel>, MVTestTriggeredModelMapper>()
            .AddSingleton<IEventModelMapper<PersonalizationEventModel>, PersonalizationEventModelMapper>()
            .AddSingleton<IEventModelMapper<BounceEventModel>, BounceEventModelMapper>()
            .AddSingleton<IEventModelMapper<DispatchFailedEventModel>, DispatchFailedEventModelMapper>()
            .AddSingleton<IEventModelMapper<EmailClickedEventModel>, EmailClickedEventModelMapper>()
            .AddSingleton<IEventModelMapper<EmailOpenedEventModel>, EmailOpenedEventModelMapper>()
            .AddSingleton<IEventModelMapper<EmailSentEventModel>, EmailSentEventModelMapper>()
            .AddSingleton<IEventModelMapper<SpamComplaintEventModel>, SpamComplaintEventModelMapper>()
            .AddSingleton<IEventModelMapper<UnsubscribedFromEmailEventModel>, UnsubscribedFromEmailEventModelMapper>()
            .AddSingleton<IEventModelMapper<EmailEventModel>, EmailEventModelMapper>()
            .BuildServiceProvider();
    }
```

## Custom model migration
This section contains step-by-step guidance on how to update the code sample to migrate custom model facets and events.

The code sample needs to reference the assembly with the custom model.

### XConnect client configuration
The code sample uses the concept of `XConnectReader` and `XConnectWriter`. Both of them configure the XConnect Client to work with the proper version of XConnect.

The custom model must be specified for the XConnect Client Configuration for both `XConnectReader` and `XConnectWriter`.

```csharp
    XdbModel[] models =
    {
        CollectionModel.Model,
        EmailCollectionModel.Model,
        CustomDataModel.Model,
        Sample.Collection.Model.CustomModel.Model
    };

    return new XConnectClientConfiguration(
        new XdbRuntimeModel(models),
        collectionClient,
        searchClient,
        configurationClient,
        true);
```

### Custom facet migration
The list of contact and interaction custom facet keys needs to be specified in the configuration file. For more information, please refer to the [Contacts and Interaction Facets](###Contacts-and-Interaction-Facets)

Custom model difference between source and target XConnect versions:
* If there are no changes, the migration process will handle custom migration automatically. No other changes are required.
* If there are any changes in the custom facet model, you must register the custom facet model and facet mappers. Please refer to the [Customize core facets migration](###Customize-core-facets-migration).

### Custom interaction event migration
All custom interaction events that must be migrated require an event model and event mappers to be registered explicitly. Please refer to the [Interaction Events](###Interaction-Events).

## Limitations
The following list details the limitations of the data migration code sample:
* ****Contacts****
  * **Contact id** will differ, the system generates contact ids sequentially and they cannot be specified for the target contact explicitly.
  * **LastModified** will differ, the system generates last modified and it cannot be specified explicitly.
  * **Percentile** value will differ, the percentile value is a system property which is based on contact id, so it cannot be specified explicitly.
  * ****Contact identifiers****
    * **Alias** identifier is a system identifier that cannot be migrated with the `Alias` source, so to keep the reference between source and target contacts the new source `AliasOld` is used to migrate the source alias value.
    * **Merge** identifier is a system identifier that is skipped from migration explicitly with the help of identifier filters, because the merged contacts are not migrated by default.
  * Merged contacts are not migrated. Only target contacts that contain all the relevant contact data are migrated.
    * We do not recommend that you disable this filter.
  * Forgotten contacts are not migrated.
    * This filter can be disabled in the code sample if you need this data in the target instance.
  * Anonymous contacts are not migrated.
    * This filter can be disabled in the code sample if you need this data in the target instance.
  * **Calculated facets** are not migrated explicitly. They are calculated by the target XConnect service layer when a contact's interactions are migrated.
* ****Interactions****
  * **Interaction id** will differ. The system generates interaction ids sequentially and they cannot be specified for the target interaction explicitly.
  * **LastModified** will differ. The system generates last modified and it cannot be specified explicitly.
  * **Percentile** value will differ. The percentile value is a system property that is based on interaction id, so it cannot be specified explicitly.
  * **Merged interactions** are not migrated because they are part of the merged contact, which is skipped by the contact filter. These interactions are migrated with the target contact.
* ****Device Profiles****
  * Device Profiles are not migrated.

The data distribution between the shards will differ. This means that the migrated contact might not be located in the same shard as in the source XConnect even if number of shards is the same.

## Exceptions
The data migration in the code sample can be run multiple times in case there are issues that occur during the migration process.

The contact data extraction returns all the data and it cannot filter data that has been already migrated, so the list of contacts that is already merged will be extracted, but they will not be duplicated in the target instance.
The alias identifier, which is migrated together with the contact as `AliasOld` identifier, prevents duplicates. The 'AlreadyExist' failure will be traced if the tool tries to submit a contact that has been migrated already.


# Contribute
TODO: Explain how other users and developers can contribute to make your code better.
TODO: Will be completed later
